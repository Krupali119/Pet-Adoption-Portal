namespace PetAdoptionMVC.Models
{
    public class PaymentOptionsViewModel
    {
        public int RequestId { get; set; }
        public decimal Amount { get; set; }
        public List<string> PaymentMethods { get; set; }
    }

}
