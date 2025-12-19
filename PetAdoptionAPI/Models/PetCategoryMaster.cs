using System.ComponentModel.DataAnnotations;

public class PetCategoryMaster
{
    [Key]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Category Name is required.")]
    [StringLength(50, ErrorMessage = "Category Name can't be longer than 50 characters.")]
    public string CategoryName { get; set; }

    public bool ActiveFlag { get; set; } = true;
}
