using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.Services.Core.Service.Models.Requests
{
    public class CreateAccountRequest
    {
        public string CustomerId { get; set; }
        public decimal InitialCredit { get; set; }
    }
}
