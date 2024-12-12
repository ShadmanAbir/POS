using System.ComponentModel.DataAnnotations;

namespace UtopiaCatering.Models
{
    public class Journals : BaseEntity
    {
        [Key]
        public int JournalID { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public int Type { get; set; }
        public int Category { get; set; }
        public string Description { get; set; }
        public int? ItemID { get; set; }
        public decimal Balance { get; set; }
    }
}
