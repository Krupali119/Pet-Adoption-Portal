using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class AdoptionMaster
{
    [Key]
    public int AdoptionId { get; set; }

    [Required(ErrorMessage = "Request is required.")]
    public int RequestId { get; set; }
    [ForeignKey("RequestId")]

    [Required(ErrorMessage = "Adoption Date is required.")]
    public DateTime AdoptionDate { get; set; }

    [StringLength(20, ErrorMessage = "Final Status can't be longer than 20 characters.")]
    public string FinalStatus { get; set; }

    [Required(ErrorMessage = "Adopter is required.")]

    public int AdopterId { get; set; }
    [ForeignKey("AdopterId")]

    // Navigation Properties
    //public AdoptionRequest AdoptionRequest { get; set; }
    public UserMaster Adopter { get; set; }
}
