using AccountsManager.Services.Core.Data.Contexts;
using AccountsManager.Services.Core.Data.Models;
using AccountsManager.Services.Core.Data.Repositories;
using AccountsManager.Services.Core.Service.Contracts;
using AccountsManager.Services.Core.Service.Models.Mapping;
using AccountsManager.Services.Core.Service.Models.Requests;
using AccountsManager.Services.Core.Service.Models.Responses;
using AccountsManager.Services.Core.Service.Services;
using AccountsManager.Services.Core.Test.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace AccountsManager.Services.Core.Test.UnitTests
{
    public class AccountTests
    {
        private readonly Mock<IRequestInfoService> _mockRequestInfoService = new();
        private readonly UnitOfWork _mockUnitOfWork;
        private readonly CoreDbContext _mockCoreDbContext;
        private readonly IMapper _mockMapper;
        private readonly CustomerService _mockCustomerService;
        private readonly AccountService _mockAccountService;
        private readonly TransactionService _mockTransactionService;
        private readonly DataHelper _dataHelper;

        public AccountTests()
        {
            var _dbContextOptions = new DbContextOptionsBuilder<CoreDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _mockCoreDbContext = new CoreDbContext(_dbContextOptions);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            _mockMapper = mockMapper.CreateMapper();

            _mockUnitOfWork = new(_mockCoreDbContext);
            _mockCustomerService = new(_mockUnitOfWork, _mockMapper, _mockRequestInfoService.Object);
            _mockTransactionService = new(_mockUnitOfWork, _mockRequestInfoService.Object);
            _mockAccountService = new(_mockUnitOfWork, _mockMapper, _mockRequestInfoService.Object, _mockTransactionService);
            _dataHelper = new();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            var customer = _dataHelper.CustomerModel();
            var statuses = _dataHelper.StatusesModel();
            _mockCoreDbContext.Customer.Add(customer);
            _mockCoreDbContext.Status.AddRange(statuses);
            _mockCoreDbContext.SaveChanges();
        }

        [Fact]
        public void GetCustomerInfo_Success()
        {
            //ARRANGE
            var request = new GetCustomerInfoRequest
            {
                CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac1"
            };
            var response = _dataHelper.GetCustomerInfoResponseModel();

            //ACT
            var result = _mockCustomerService.GetCustomerInfo(request.CustomerId);

            //ASSERT
            Assert.NotNull(result);
            Assert.Equal(JsonConvert.SerializeObject(response), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public void CreateCustomerAccount_Success()
        {
            //ARRANGE
            var request = new CreateAccountRequest
            {
                CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac1",
                AccountDescription = "Account Description",
                InitialCredit = 0
            };

            //ACT
            var result = _mockAccountService.CreateAccount(request.CustomerId, request.AccountDescription, request.InitialCredit);

            //ASSERT
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateCustomerAccount_InitialCreditAdded()
        {
            //ARRANGE
            var request = new CreateAccountRequest
            {
                CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac1",
                AccountDescription = "Account Description",
                InitialCredit = 100.00M
            };

            //ACT
            var result = _mockAccountService.CreateAccount(request.CustomerId, request.AccountDescription, request.InitialCredit);

            //ASSERT
            Assert.NotNull(result);
            Assert.Single(result.Account.Transactions);
            Assert.Equal("100.00", result.Account.AccountBalance);
        }

        [Fact]
        public void CreateCustomerAccount_ZeroCredit_TransactionNotCreated()
        {
            //ARRANGE
            var request = new CreateAccountRequest
            {
                CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac1",
                AccountDescription = "Account Description",
                InitialCredit = 0
            };

            //ACT
            var result = _mockAccountService.CreateAccount(request.CustomerId, request.AccountDescription, request.InitialCredit);

            //ASSERT
            Assert.NotNull(result);
            Assert.True(result.Account.Transactions.Count == 0);
        }

        [Fact]
        public void CreateCustomerAccount_InvalidCustomerId()
        {
            //ARRANGE
            var request = new CreateAccountRequest
            {
                CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac0",
                AccountDescription = "Account Description",
                InitialCredit = 0
            };

            //ACT
            var result = _mockAccountService.CreateAccount(request.CustomerId, request.AccountDescription, request.InitialCredit);

            //ASSERT
            Assert.Null(result);
        }

        [Fact]
        public void AddAccountTransaction_Success()
        {
            //ARRANGE
            var request = new AddAccountTransactionRequest
            {
                CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac1",
                AccountNumber = "000001-001",
                TransactionDescription = "Description",
                Amount = 100.00M
            };

            //ACT
            var result = _mockAccountService.AddAccountTransaction(request.CustomerId,
                                                                   request.AccountNumber,
                                                                   request.TransactionDescription,
                                                                   request.Amount);

            //ASSERT
            Assert.NotNull(result);
        }

        [Fact]
        public void AddAccountTransaction_ValidAccountBalance()
        {
            //ARRANGE
            var request = new AddAccountTransactionRequest
            {
                CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac1",
                AccountNumber = "000001-001",
                TransactionDescription = "Description",
                Amount = 100.00M
            };

            //ACT
            var result = _mockAccountService.AddAccountTransaction(request.CustomerId, 
                                                                   request.AccountNumber, 
                                                                   request.TransactionDescription, 
                                                                   request.Amount);

            //ASSERT
            Assert.NotNull(result);
            Assert.True(result.Transaction.ClosingBalance == "194.42");
        }

        [Fact]
        public void AddAccountTransaction_InvalidAccountNumber()
        {
            //ARRANGE
            var request = new AddAccountTransactionRequest
            {
                CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac1",
                AccountNumber = "000001-100",
                TransactionDescription = "Description",
                Amount = 100.00M
            };

            //ACT
            var result = _mockAccountService.AddAccountTransaction(request.CustomerId,
                                                                   request.AccountNumber,
                                                                   request.TransactionDescription,
                                                                   request.Amount);

            //ASSERT
            Assert.Null(result);
        }

        [Fact]
        public void AddAccountTransaction_InvalidCustomerId()
        {
            //ARRANGE
            var request = new AddAccountTransactionRequest
            {
                CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac0",
                AccountNumber = "000001-001",
                TransactionDescription = "Description",
                Amount = 100.00M
            };

            //ACT
            var result = _mockAccountService.AddAccountTransaction(request.CustomerId,
                                                                   request.AccountNumber,
                                                                   request.TransactionDescription,
                                                                   request.Amount);

            //ASSERT
            Assert.Null(result);
        }

        [Fact]
        public void AddAccountTransaction_ZeroAmount()
        {
            //ARRANGE
            var request = new AddAccountTransactionRequest
            {
                CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac1",
                AccountNumber = "000001-001",
                TransactionDescription = "Description",
                Amount = 0
            };

            //ACT
            var result = _mockAccountService.AddAccountTransaction(request.CustomerId,
                                                                   request.AccountNumber,
                                                                   request.TransactionDescription,
                                                                   request.Amount);

            //ASSERT
            Assert.Null(result);
        }

        [Fact]
        public void GetCustomerInfo_ValidBalance()
        {
            //ARRANGE
            var request = new AddAccountTransactionRequest
            {
                CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac1",
                AccountNumber = "000001-001",
                TransactionDescription = "Description",
                Amount = 100.00M
            };

            var customerInfoRequest = new GetCustomerInfoRequest
            {
                CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac1"
            };
            var customerInfoResponse = _dataHelper.GetCustomerInfoResponseModel();

            //ACT
            var result = _mockAccountService.AddAccountTransaction(request.CustomerId,
                                                                   request.AccountNumber,
                                                                   request.TransactionDescription,
                                                                   request.Amount);

            var customerInfoResult = _mockCustomerService.GetCustomerInfo(request.CustomerId);

            //ASSERT
            Assert.NotNull(result);
            Assert.NotNull(customerInfoResult);
            Assert.Equal("194.42", customerInfoResult.CustomerInfo.Balance);
        }

        [Fact]
        public void GetCustomers_Success()
        {
            //ARRANGE
            var request = new BaseRequest
            {
                PageNumber = 1,
                PageSize = 10
            };

            //ACT
            var result = _mockCustomerService.GetCustomers(request.PageSize, request.PageNumber);

            //ASSERT
            Assert.NotNull(result);
            Assert.True(result.TotalCount > 0);
        }

        [Fact]
        public void CreateCustomer_Success()
        {
            //ARRANGE
            var request = new CreateCustomerRequest
            {
                Name = "Customer Name",
                Surname = "Customer Surname"
            };

            //ACT
            var result = _mockCustomerService.CreateCustomer(request.Name, request.Surname);

            //ASSERT
            Assert.NotNull(result);
        }
    }
}