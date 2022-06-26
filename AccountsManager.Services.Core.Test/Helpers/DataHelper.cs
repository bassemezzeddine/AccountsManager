using AccountsManager.Services.Core.Data.Enums;
using AccountsManager.Services.Core.Data.Models;
using AccountsManager.Services.Core.Service.Models.Responses;
using System;
using System.Collections.Generic;

namespace AccountsManager.Services.Core.Test.Helpers
{
    public class DataHelper
    {
        public Customer CustomerModel()
        {
            var userId = new Guid("d81d32ba-841e-4c9c-ba9c-50f532845b4f");
            return new Customer
            {
                Name = "Bassem",
                Surname = "Ezzeddine",
                Reference = new Guid("442be8d3-cf31-4ab6-a52e-9ecf97107ac1"),
                StatusId = (int)StatusEnum.Active,
                CreatedBy = userId,
                CreatedOn = new DateTime(2022, 1, 1),
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountNumber = "000001-001",
                        Balance = 94.42M,
                        Description = "Account 01",
                        StatusId = (int)StatusEnum.Active,
                        CreatedBy = userId,
                        CreatedOn = new DateTime(2022, 1, 1),
                        Transactions = new List<Transaction>
                        {
                            new Transaction
                            {
                                Reference = new Guid("05cb0e53-746b-4c9d-b18c-93c0bd625015").ToString(),
                                Amount = -1.10M,
                                OpeningBalance = 95.52M,
                                ClosingBalance = 94.42M,
                                Description = "Transaction 02",
                                StatusId = (int)StatusEnum.Completed,
                                CreatedBy = userId,
                                CreatedOn = new DateTime(2022, 1, 1)
                            },
                            new Transaction
                            {
                                Reference = new Guid("d52893e7-bcc0-4b99-9d13-4e4e6f1af9f0").ToString(),
                                Amount = 95.52M,
                                OpeningBalance = 0.00M,
                                ClosingBalance = 95.52M,
                                Description = "Transaction 01",
                                StatusId = (int)StatusEnum.Completed,
                                CreatedBy = userId,
                                CreatedOn = new DateTime(2022, 1, 1)
                            }
                        }
                    }
                }
            };
        }

        public List<Status> StatusesModel()
        {
            return new List<Status>
            {
                new Status
                {
                    Id = 1,
                    Name = "Active"
                },
                new Status
                {
                    Id = 2,
                    Name = "Inactive"
                },
                new Status
                {
                    Id= 3,
                    Name = "Closed"
                },
                new Status
                {
                    Id= 4,
                    Name = "Completed"
                }
            };
        }

        public GetCustomerInfoResponse GetCustomerInfoResponseModel()
        {
            return new GetCustomerInfoResponse
            {
                CustomerInfo = new CustomerDTO
                {
                    Name = "Bassem",
                    Surname = "Ezzeddine",
                    CustomerId = "442be8d3-cf31-4ab6-a52e-9ecf97107ac1",
                    Balance = "94.42",
                    Status = "Active",
                    CreationDate = new DateTime(2022, 1, 1),
                    Accounts = new List<AccountDTO>
                    {
                        new AccountDTO
                        {
                            AccountNumber = "000001-001",
                            AccountBalance = "94.42",
                            Description = "Account 01",
                            Status = "Active",
                            CreationDate = new DateTime(2022, 1, 1),
                            Transactions = new List<TransactionDTO>
                            {
                                new TransactionDTO
                                {
                                    Reference = new Guid("05cb0e53-746b-4c9d-b18c-93c0bd625015").ToString(),
                                    Amount = "-1.10",
                                    OpeningBalance = "95.52",
                                    ClosingBalance = "94.42",
                                    Description = "Transaction 02",
                                    CreationDate = new DateTime(2022, 1, 1)
                                },
                                new TransactionDTO
                                {
                                    Reference = new Guid("d52893e7-bcc0-4b99-9d13-4e4e6f1af9f0").ToString(),
                                    Amount = "95.52",
                                    OpeningBalance = "0.00",
                                    ClosingBalance = "95.52",
                                    Description = "Transaction 01",
                                    CreationDate = new DateTime(2022, 1, 1)
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
