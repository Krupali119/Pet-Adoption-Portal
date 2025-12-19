using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoptionMVC.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required(ErrorMessage = "Payment Date is required.")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Payment Amount is required.")]
        public decimal PaymentAmount { get; set; }

        [Required(ErrorMessage = "Transaction ID is required.")]
        [StringLength(100, ErrorMessage = "Transaction ID can't be longer than 100 characters.")]
        public string TransactionId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Payment Method can't be longer than 50 characters.")]
        public string PaymentMethod { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Payment Status can't be longer than 20 characters.")]
        public string PaymentStatus { get; set; } = "Pending";


        public bool ActiveFlag { get; set; } = true;

        // Navigation Properties

        //public int? DonationId { get; set; }  = null;   
        //[ForeignKey("DonationId")]
        //public virtual Donation? Donation { get; set; }

        public int? CardPaymentId { get; set; }
        [ForeignKey("CardPaymentId")]
        public virtual CardPayment? CardPayment { get; set; }

        public int? WhatsAppPaymentId { get; set; }
        [ForeignKey("WhatsAppPaymentId")]
        public virtual WhatsAppPayment? WhatsAppPayment { get; set; }
    }
}