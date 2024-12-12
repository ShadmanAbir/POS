using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UtopiaCatering.Enums;

namespace UtopiaCatering.Models
{
    public class Items : BaseEntity
    {
        [Key]
        public int ItemID { get; set; }
        public required string ItemName { get; set; }
        public int ItemType { get; set; }
        public decimal Balance { get; set; }

        //navigation properties
        public ICollection<ItemElements>? ItemElements { get; set; }

        [NotMapped]
        public string? ItemTypeName => Enum.GetName(typeof(ItemType), ItemType);
    }

    public class ItemElements : BaseEntity
    {
        [Key]
        public int ItemElementID { get; set; }
        public int ItemID { get; set; }
        public int ElementID { get; set; }
        public decimal Quantity { get; set; }
        public int Unit { get; set; }

        //Navigation properties
        public Items? Items { get; set; }

        [NotMapped]
        public string? ElementsName { get; set; }
    }
}
