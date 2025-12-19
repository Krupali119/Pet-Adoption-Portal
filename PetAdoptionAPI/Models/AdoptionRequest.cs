using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class AdoptionRequest
{
    [Key]
    public int RequestId { get; set; }


    [Required(ErrorMessage = "Request Date is required.")]
    public DateTime RequestDate { get; set; }

    [StringLength(20, ErrorMessage = "Status can't be longer than 20 characters.")]
    public string Status { get; set; }

    public bool ActiveFlag { get; set; } = true;

    // Navigation Properties

    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual UserMaster User { get; set; }

    public int PetId { get; set; }
    [ForeignKey("PetId")]
    public virtual PetMaster Pet { get; set; }

}
