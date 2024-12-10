using System.ComponentModel.DataAnnotations;

namespace UtopiaCatering.Models
{
    public class Items
    {
        [Key]
        public int ItemID { get; set; }
        public required string ItemName { get; set; }
        public int ItemType { get; set; }
        public decimal Balance { get; set; }
    }
}
