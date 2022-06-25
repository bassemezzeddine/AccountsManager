using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.Services.Core.Data.Models
{
    public class AccountDTO
    {
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public string AccountBalance { get; set; }
        public List<TransactionDTO> Transactions { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
    }
}
