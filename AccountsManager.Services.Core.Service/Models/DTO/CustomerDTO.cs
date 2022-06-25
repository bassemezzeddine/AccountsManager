using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.Services.Core.Data.Models
{
    public class CustomerDTO
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Balance { get; set; }
        public List<AccountDTO> Accounts { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
    }
}
