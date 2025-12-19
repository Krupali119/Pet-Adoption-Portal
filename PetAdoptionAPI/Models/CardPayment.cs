using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CardPayment
{
    [Key]
    public int CardPaymentId { get; set; }


    [Required(ErrorMessage = "Card Holder Name is required.")]
    [StringLength(100, ErrorMessage = "Card Holder Name can't be longer than 100 characters.")]
    public string CardHolderName { get; set; }

    [Required(ErrorMessage = "Card Number is required.")]
    [StringLength(16, MinimumLength = 16, ErrorMessage = "Card Number must be 16 digits.")]
    public string CardNumber { get; set; }

    [Required(ErrorMessage = "Expiration Date is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Expiration Date format must be MM/YY.")]
    public string ExpirationDate { get; set; }

    [Required(ErrorMessage = "CVV is required.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV must be 3 digits.")]
    public string CVV { get; set; }

    public bool SaveCard { get; set; }

    public bool ActiveFlag { get; set; } = true;

    
    //public int PaymentId { get; set; }
    //[ForeignKey("PaymentId")]
    //public virtual Payment Payment { get; set; }
}
