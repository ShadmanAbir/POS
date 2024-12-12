using System.ComponentModel.DataAnnotations;

namespace UtopiaCatering.Models
{
    public class WorkOrder : BaseEntity
    {
        [Key]
        public int WoID { get; set; }
        public int OrganizationID { get; set; }
        public int OrganizerID { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal Vat { get; set; }
        public decimal Tax { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsCompleted { get; set; }

        //navigation properties
        public ICollection<WorkOrderDetails> WorkOrderDetails { get; set; }
        public ICollection<WorkOrderWiseEvents> WorkOrderWiseEvents { get; set; }

    }

    public class WorkOrderDetails : BaseEntity
    {
        [Key]
        public int WorkOrderDetailsID { get; set; }
        public int WoID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        //navigation properties
        public WorkOrder WorkOrder { get; set; }
        public Items Items { get; set; }
    }

    public class WorkOrderWiseEvents :BaseEntity
    {
        [Key]
        public int WorkOrderWiseEventsID { get; set; }
        public int EventID { get; set; }
        public int WorkOrderID { get; set; }

        public WorkOrder WorkOrder { get; set; }
    }
}
