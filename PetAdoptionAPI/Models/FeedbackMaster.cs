using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class FeedbackMaster
{
    [Key]
    public int FeedbackId { get; set; }

    [Required(ErrorMessage = "Feedback is required.")]
    public string Feedback { get; set; }

    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "Created Date is required.")]
    public DateTime CreatedDate { get; set; }

    public bool ActiveFlag { get; set; } = true;

    // Navigation Property
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual UserMaster UserMaster { get; set; }
}
