using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoptionMVC.Models
{
    public class NotificationMaster
    {
        [Key]
        public int NotificationId { get; set; }

        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedDate { get; set; }

        public bool ActiveFlag { get; set; } = true;

        // Navigation Property
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserMaster UserMaster { get; set; }
    }
}