using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoptionMVC.Models
{
    public class PetBehaviorProfile
    {
        [Key]
        public int BehaviorProfileId { get; set; }

        [Required]
        public string Behavior { get; set; }

        [Required]
        public string Training { get; set; }

        public bool ActiveFlag { get; set; } = true;

        // Navigation Property
        public int PetId { get; set; }
        [ForeignKey("PetId")]
        public virtual PetMaster PetMaster { get; set; }


    }
}