namespace PetAdoptionMVC.Models
{
    public class CombinedViewModel
    {
        public IEnumerable<PetMaster> Petdetails { get; set; }
        public IEnumerable<PetBehaviorProfile> behavior { get; set; }
        public IEnumerable<PetMedicalRecords> medical { get; set; }
        public IEnumerable<AdoptionRequest> adoption { get; set; }
    }
}
