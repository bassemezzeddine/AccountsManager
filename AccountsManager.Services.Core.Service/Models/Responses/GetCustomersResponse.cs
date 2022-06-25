using AccountsManager.Services.Core.Data.Models;
using AccountsManager.Services.Core.Service.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.Services.Core.Service.Models.Responses
{
    public class GetCustomersResponse : BaseResponse
    {
        public List<CustomerListDTO> Customers { get; set; }
    }
}
