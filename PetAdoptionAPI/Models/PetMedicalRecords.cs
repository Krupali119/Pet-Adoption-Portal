using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PetMedicalRecords
{
    [Key]
    public int MedicalRecordId { get; set; }

    [Required]
    public string VaccinationDetails { get; set; }

    [Required]
    public string HealthHistory { get; set; }

    public bool ActiveFlag { get; set; } = true;

    // Navigation Property
    public int PetId { get; set; }
    [ForeignKey("PetId")]
    public virtual PetMaster PetMaster { get; set; }

}
