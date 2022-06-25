using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.Services.Core.Service.Models.Requests
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
