using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.Services.Core.Data.Models
{
    public class TransactionDTO
    {
        public string Reference { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string OpeningBalance { get; set; }
        public string ClosingBalance { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
