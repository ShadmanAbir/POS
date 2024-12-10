using System.ComponentModel.DataAnnotations;

namespace UtopiaCatering.Models
{
    public class ItemElements
    {
        [Key]
        public int ItemElementID { get; set; }
        public int ItemID { get; set; }
        public int ElementID { get; set; }
        public decimal Quantity { get; set; }
        public int Unit { get; set; }
        
    }
}
