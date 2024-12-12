using System.ComponentModel.DataAnnotations;

namespace UtopiaCatering.Models
{
    public class Events : BaseEntity
    {
        [Key]
        public int EventID { get; set; }
        public required string EventName { get; set; }
        public int OrganizationID { get; set; }
        // Navigation Property
        public Organization? Organization { get; set; }  // Link to Organization
    }
}
