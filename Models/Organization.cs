using System.ComponentModel.DataAnnotations;
using UtopiaCatering.Models;

namespace UtopiaCatering.Models
{
    public class Organization :BaseEntity
    {
        [Key]
        public int OrganizationID { get; set; }
        public required string OrganizationName { get; set; }

        public ICollection<Events>? Events { get; set; }  // Organization can have many events
        public ICollection<Organizer>? Organizers { get; set; }  // Organization can have many organizers
        public ICollection<PurchaseOrder>? PurchaseOrders { get; set; }
    }



    public class Organizer : BaseEntity
    {
        [Key]
        public int OrganizerID { get; set; }
        public int OrganizationID { get; set; }
        public required string OrganizerName { get; set; }
        // Navigation Property
        public Organization Organization { get; set; }  // Link to Organization

    }
}
