using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PetMaster
{
    [Key]
    public int PetId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Pet Name can't be longer than 100 characters.")]
    public string PetName { get; set; }


    [Required]
    [Range(0, 100, ErrorMessage = "Age must be between 0 and 100.")]
    public int Age { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "Gender can't be longer than 10 characters.")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0, 10000, ErrorMessage = "Price must be between 0 and 10,000.")]
    public decimal Price { get; set; }

    // Field for Pet Image Upload
    // This prevents the field from being mapped to the database
    [Required(ErrorMessage = "Please upload a pet image.")]
    public string PetImage { get; set; }

    [Required]
    public string Description { get; set; }


    [Required]
    [StringLength(20, ErrorMessage = "Status can't be longer than 20 characters.")]
    public string Status { get; set; }

    public bool ActiveFlag { get; set; } = true;


    // Navigation Properties

    public int PetCategoryId { get; set; }
    [ForeignKey("PetCategoryId")]
    public virtual PetCategoryMaster PetCategoryMaster { get; set; }

    [NotMapped]
    public IFormFile ImageFile { get; set; }

}
