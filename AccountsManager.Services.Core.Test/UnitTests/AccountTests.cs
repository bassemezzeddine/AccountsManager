using AccountsManager.Controllers;
using AccountsManager.Services.Core.API.Contracts;
using AccountsManager.Services.Core.Data.Contexts;
using AccountsManager.Services.Core.Data.Contracts;
using AccountsManager.Services.Core.Data.Enums;
using AccountsManager.Services.Core.Data.Models;
using AccountsManager.Services.Core.Data.Repositories;
using AccountsManager.Services.Core.Service.Contracts;
using AccountsManager.Services.Core.Service.Models.Custom;
using AccountsManager.Services.Core.Service.Models.Mapping;
using AccountsManager.Services.Core.Service.Models.Requests;
using AccountsManager.Services.Core.Service.Models.Responses;
using AccountsManager.Services.Core.Service.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public AccountTests()
        {
            var _dbContextOptions = new DbContextOptionsBuilder<CoreDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _mockCoreDbContext = new CoreDbContext(_dbContextOptions);
            _mockUnitOfWork = new(_mockCoreDbContext);
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            _mockMapper = mockMapper.CreateMapper();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            var userId = new Guid("d81d32ba-841e-4c9c-ba9c-50f532845b4f");
            var customer = new Customer
            {
                Name = "Bassem",
                Surname = "Ezzeddine",
                Reference = new Guid("442BE8D3-CF31-4AB6-A52E-9ECF97107AC1"),
                StatusId = (int)StatusEnum.Active,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow,
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountNumber = "000001-001",
                        Balance = 94.42M,
                        Description = "Account 01",
                        StatusId = (int)StatusEnum.Active,
                        CreatedBy = userId,
                        CreatedOn = DateTime.UtcNow,
                        Transactions = new List<Transaction>
                        {
                            new Transaction
                            {
                                Reference = Guid.NewGuid().ToString(),
                                Amount = -1.10M,
                                OpeningBalance = 95.52M,
                                ClosingBalance = 94.42M,
                                Description = "Transaction 02",
                                StatusId = (int)StatusEnum.Completed,
                                CreatedBy = userId,
                                CreatedOn = DateTime.UtcNow
                            },
                            new Transaction
                            {
                                Reference = Guid.NewGuid().ToString(),
                                Amount = 95.52M,
                                OpeningBalance = 0.00M,
                                ClosingBalance = 95.52M,
                                Description = "Transaction 01",
                                StatusId = (int)StatusEnum.Completed,
                                CreatedBy = userId,
                                CreatedOn = DateTime.UtcNow
                            }
                        }
                    }
                }
            };
            _mockCoreDbContext.Customer.Add(customer);
            _mockCoreDbContext.SaveChanges();
        }

        [Fact]
        public void Test1()
        {
            //ARRANGE
            var serviceResponse = new GetCustomersResponse
            {
                Customers = new List<CustomerListDTO>
                {
                    new CustomerListDTO
                    {
                        Name = "Bassem",
                        Surname = "Ezzeddine",
                        CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac1"
                    }
                },
                TotalCount = 1
            };
            var request = new BaseRequest
            { 
                PageNumber = 1,
                PageSize = 10
            };            
            CustomerService customerService = new CustomerService(_mockUnitOfWork,
                                                                  _mockMapper,
                                                                  _mockRequestInfoService.Object);
            //ACT
            var result = customerService.GetCustomers(request.PageSize, request.PageNumber);

            //ASSERT
            Assert.Equal(JsonConvert.SerializeObject(serviceResponse), JsonConvert.SerializeObject(result));
        }
    }
}