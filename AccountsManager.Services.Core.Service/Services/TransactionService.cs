using AutoMapper;
using AccountsManager.Services.Core.Service.Contracts;
using AccountsManager.Services.Core.Data.Contracts;
using AccountsManager.Services.Core.Data.Repositories;
using AccountsManager.Services.Core.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using AccountsManager.Services.Core.Data.Enums;

namespace AccountsManager.Services.Core.Service.Services
{
    public class TransactionService : ITransactionService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly TransactionRepo _transactionRepo;
        private readonly IRequestInfoService _requestInfoService;

        public TransactionService(IUnitOfWork unitOfWork,
                                  IRequestInfoService requestInfoService)
        {
            _unitOfWork = unitOfWork;
            _transactionRepo = _unitOfWork.TransactionRepo;
            _requestInfoService = requestInfoService;
        }
        #endregion

        #region Methods
        public bool AddInitialCredit(int accountId, decimal initialCredit)
        {
            var transaction = new Transaction
            {
                AccountId = accountId,
                Amount = initialCredit,
                OpeningBalance = 0,
                ClosingBalance = initialCredit,
                Reference = Guid.NewGuid().ToString(),
                StatusId = (int)StatusEnum.Completed,
                Description = "Account opening balance transaction"
            };
            _transactionRepo.Insert(transaction, _requestInfoService.UserId);
            return true;
        }

        public Transaction AddTransaction(int accountId, string description, decimal openingBalance, decimal amount)
        {
            var reference = Guid.NewGuid().ToString();
            var transaction = new Transaction
            {
                AccountId = accountId,
                Amount = amount,
                OpeningBalance = openingBalance,
                ClosingBalance = openingBalance + amount,
                Reference = reference,
                StatusId = (int)StatusEnum.Completed,
                Description = description
            };
            _transactionRepo.Insert(transaction, _requestInfoService.UserId);
            return transaction;
        }
        #endregion
    }
}
