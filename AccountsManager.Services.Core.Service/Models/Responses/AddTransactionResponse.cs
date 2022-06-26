using AccountsManager.Services.Core.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.Services.Core.Service.Models.Responses
{
    public class AddTransactionResponse
    {
        public TransactionDTO Transaction { get; set; }
    }
}
