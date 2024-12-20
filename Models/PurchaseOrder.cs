﻿using System.ComponentModel.DataAnnotations;

namespace UtopiaCatering.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int PoID { get; set; }
        public int VendorID { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal Due { get; set; }
        public decimal PaidAmount { get; set; }

        public virtual Vendor? Vendor { get; set; }
        public virtual ICollection<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }

    }

    public class PurchaseOrderDetails
    {
        [Key]
        public int PoDetailsID { get; set; }
        public int PoID { get; set; }
        public int ItemID { get; set; }
        public int Unit { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }

        // Navigation Property
        public virtual PurchaseOrder? PurchaseOrder { get; set; }  // Link to PurchaseOrder

    }

}
