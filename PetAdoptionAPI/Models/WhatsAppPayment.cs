using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class WhatsAppPayment
{
    [Key]
    public int WhatsAppPaymentId { get; set; }

    [Required]
    [Phone(ErrorMessage = "Invalid contact number.")]
    [StringLength(10, ErrorMessage = "Contact Number can't be longer than 10 characters.")]
    public string UserPhoneNumber { get; set; }

    [Required]
    [StringLength(100)]
    public string WhatsAppTransactionId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [StringLength(20)]
    public string PaymentStatus { get; set; } = "Pending";

    public bool ActiveFlag { get; set; } = true;

    
    //public int PaymentId { get; set; }
    //[ForeignKey("PaymentId")]
    //public virtual Payment Payment { get; set; }
}
