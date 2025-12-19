using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserMaster
{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters.")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Full Name can't be longer than 100 characters.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(8, ErrorMessage = "Password can't be longer than 8 characters.")]
    //[RegularExpression(@"^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[@$!%?&])[A-Za-z\d@$!%?&]{8,}$",
    //ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    public string Password { get; set; }

    [Required]
    [Phone(ErrorMessage = "Invalid contact number.")]
    [StringLength(10, ErrorMessage = "Contact Number can't be longer than 10 characters.")]
    public string ContactNo { get; set; }

    public bool ActiveFlag { get; set; } = true;

    public int RoleId { get; set; }
    [ForeignKey("RoleId")]
    public virtual RoleMaster RoleMaster { get; set; }
}
