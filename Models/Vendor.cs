﻿namespace UtopiaCatering.Models
{
    public class Vendor : BaseEntity
    {
        public int VendorID { get; set; }
        public required string VendorName { get; set; }

        //Navigation Property
        public ICollection<PurchaseOrder>? PurchaseOrders { get; set; }
    }
}
