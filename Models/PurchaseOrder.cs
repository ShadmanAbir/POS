namespace UtopiaCatering.Models
{
    public class PurchaseOrder
    {
        public int PoID { get; set; }
        public int ItemID { get; set; }
        public int Unit { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
    }
}
