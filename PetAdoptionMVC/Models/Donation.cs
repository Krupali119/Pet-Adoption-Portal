using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoptionMVC.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, 10000, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Donation Date is required.")]
        public DateTime DonationDate { get; set; }

        public string Message { get; set; }

        public bool ActiveFlag { get; set; } = true;

        // Navigation Property
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserMaster UserMaster { get; set; }
    }
}