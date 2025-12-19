using System.ComponentModel.DataAnnotations;

namespace PetAdoptionMVC.Models
{
    public class RoleMaster
    {
        [Key]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is required.")]
        [StringLength(50, ErrorMessage = "Role Name can't be longer than 50 characters.")]
        public string RoleName { get; set; }

        public bool ActiveFlag { get; set; } = true;
    }

}
