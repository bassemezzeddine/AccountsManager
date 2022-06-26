using AutoMapper;
using AccountsManager.Services.Core.Service.Contracts;
using AccountsManager.Services.Core.Data.Contracts;
using AccountsManager.Services.Core.Data.Repositories;
using AccountsManager.Services.Core.Data.Models;
using System.Collections.Generic;
using System.Linq;
using AccountsManager.Services.Core.Service.Models.Responses;
using AccountsManager.Services.Core.Data.Enums;
using Microsoft.AspNetCore.DataProtection;
using System;

namespace AccountsManager.Services.Core.Service.Services
{
    public class CustomerService : ICustomerService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CustomerRepo _customerRepo;
        private readonly IRequestInfoService _requestInfoService;

        public CustomerService(IUnitOfWork unitOfWork,
                               IMapper mapper,
                               IRequestInfoService requestInfoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestInfoService = requestInfoService;
            _customerRepo = _unitOfWork.CustomerRepo;
        }
        #endregion

        #region Methods
        public CreateCustomerResponse CreateCustomer(string name, string surname)
        {
            var customer = new Customer
            {
                Reference = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                StatusId = (int)StatusEnum.Active
            };
            _customerRepo.Insert(customer, _requestInfoService.UserId);
            _unitOfWork.Save();

            return new CreateCustomerResponse
            {
                CustomerId = customer.Reference.ToString()
            };
        }

        public GetCustomersResponse GetCustomers(int pageSize, int pageNumber)
        {
            var result = new GetCustomersResponse
            {
                Customers = new List<CustomerListDTO>(),
                TotalCount = 0
            };
            var customers = _customerRepo.Get(pageNumber, pageSize, x => x.StatusId == (int)StatusEnum.Active)
                                         .ToList();

            if (customers != null)
            {
                result.Customers = _mapper.Map<List<CustomerListDTO>>(customers);
                result.TotalCount = _customerRepo.GetTotalCount(x => x.StatusId == (int)StatusEnum.Active);
            }
            return result;
        }

        public GetCustomerInfoResponse GetCustomerInfo(string customerId)
        {
            var id = new Guid(customerId);
            var customer = _customerRepo.Get(1, 1, 
                                             x => x.Reference == id,
                                             includeProperties: "Status,Accounts,Accounts.Transactions").FirstOrDefault();

            if (customer == null)
                return null;

            var customerInfo = _mapper.Map<CustomerDTO>(customer);
            customerInfo.Balance = customer.Accounts.Sum(x => x.Balance).ToString("F");
            return new GetCustomerInfoResponse
            {
                CustomerInfo = customerInfo
            };
        }
        #endregion
    }
}
