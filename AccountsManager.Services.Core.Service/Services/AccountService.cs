using AutoMapper;
using AccountsManager.Services.Core.Service.Contracts;
using AccountsManager.Services.Core.Data.Contracts;
using AccountsManager.Services.Core.Data.Repositories;
using AccountsManager.Services.Core.Data.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.DataProtection;
using System;
using AccountsManager.Services.Core.Data.Enums;
using AccountsManager.Services.Core.Service.Models.Responses;

namespace AccountsManager.Services.Core.Service.Services
{
    public class AccountService : IAccountService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomerRepo _customerRepo;
        private readonly AccountRepo _accountRepo;
        private readonly IRequestInfoService _requestInfoService;
        private readonly ITransactionService _transactionService;
        private readonly IDataProtector _dataProtector;

        public AccountService(IUnitOfWork unitOfWork,
                              IRequestInfoService requestInfoService,
                              ITransactionService transactionService,
                              IDataProtector dataProtector)
        {
            _unitOfWork = unitOfWork;
            _customerRepo = _unitOfWork.CustomerRepo;
            _accountRepo = _unitOfWork.AccountRepo;
            _requestInfoService = requestInfoService;
            _transactionService = transactionService;
            _dataProtector = dataProtector;
        }
        #endregion

        #region Methods
        public CreateAccountResponse CreateAccount(string customerId, string accountDescription, decimal initialCredit)
        {
            var accountNumber = string.Empty;
            int id = Convert.ToInt32(_dataProtector.Unprotect(customerId));
            var customer = _customerRepo.Get(1, 1,
                                             x => x.Id == id).FirstOrDefault();

            var accountsCount = _accountRepo.GetTotalCount(x => x.CustomerId == id);

            if (customer == null)
                return null;

            accountNumber = GenerateAccountNumber(id, accountsCount);
            var account = new Account
            {
                CustomerId = id,
                AccountNumber = accountNumber,
                Balance = initialCredit,
                Description = accountDescription,
                StatusId = (int)StatusEnum.Active
            };
            _accountRepo.Insert(account, _requestInfoService.UserId);

            if (initialCredit > 0)
            {
                bool addInitialTransaction = _transactionService.AddInitialCredit(account.Id, initialCredit);
                if (!addInitialTransaction)
                    return null; 
            }
            _unitOfWork.Save();
            return new CreateAccountResponse
            {
                AccountNumber = accountNumber
            };
        }

        public AddTransactionResponse AddAccountTransaction(string customerId, string accountNumber, string description, decimal amount)
        {
            if (amount == 0)
                return null;
            int id = Convert.ToInt32(_dataProtector.Unprotect(customerId));
            var account = _accountRepo.Get(1, 1,
                                           x => x.StatusId == (int)StatusEnum.Active &&
                                                x.CustomerId == id &&
                                                x.AccountNumber == accountNumber).FirstOrDefault();

            if (account == null)
                return null;

            var transaction = _transactionService.AddTransaction(account.Id, description, account.Balance, amount);
            if (transaction == null)
                return null;

            account.Balance = transaction.ClosingBalance;

            _unitOfWork.Save();
            return new AddTransactionResponse
            {
                TransactionReference = transaction.Reference
            };
        }

        private string GenerateAccountNumber(int customerId, int count)
        {
            count++;
            return $"{customerId:D6}-{count:D3}";
        }
        #endregion
    }
}
