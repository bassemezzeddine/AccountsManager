using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.Services.Core.Service.Models.Requests
{
    public class AddAccountTransactionRequest
    {
        public string CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionDescription { get; set; }
        public decimal Amount { get; set; }
    }
}
