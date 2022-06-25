using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsManager.Services.Core.Data.Models
{
    public class Account : BaseModel
    {
        public int Id { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public decimal Balance { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
    }
}
