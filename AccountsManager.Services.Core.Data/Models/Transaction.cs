using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsManager.Services.Core.Data.Models
{
    public class Transaction : BaseModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public decimal Amount { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
    }
}
