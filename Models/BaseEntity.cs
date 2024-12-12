using System.ComponentModel.DataAnnotations;

namespace UtopiaCatering.Models
{
    public class BaseEntity
    {
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
