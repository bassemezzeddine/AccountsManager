using AccountsManager.Services.Core.Data.Models;
using AccountsManager.Services.Core.Service.Models.Responses;
using System.Collections.Generic;

namespace AccountsManager.Services.Core.Service.Contracts
{
    public interface ICustomerService
    {
        CreateCustomerResponse CreateCustomer(string name, string surname);
        GetCustomersResponse GetCustomers(int pageSize, int pageNumber);
        GetCustomerInfoResponse GetCustomerInfo(string customerId);
    }
}
