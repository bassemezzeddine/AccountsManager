using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountsManager.Services.Core.Data.Models
{
    public class Customer : BaseModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public virtual List<Account> Accounts { get; set; }
    }
}
