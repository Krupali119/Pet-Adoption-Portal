using Microsoft.EntityFrameworkCore;
using PetAdoptionMVC.Models;

public class PetAdoptionContext : DbContext
{
    public PetAdoptionContext(DbContextOptions<PetAdoptionContext> options) : base(options)
    {
    }

    // DbSet for each model
    public DbSet<RoleMaster> RoleMasters { get; set; }
    public DbSet<UserMaster> UserMasters { get; set; }
    public DbSet<PetMaster> PetMasters { get; set; }
    public DbSet<AdoptionRequest> AdoptionRequests { get; set; }
    public DbSet<AdoptionMaster> AdoptionMasters { get; set; }
    public DbSet<PetCategoryMaster> PetCategoryMasters { get; set; }
    public DbSet<PetMedicalRecords> PetMedicalRecords { get; set; }
    public DbSet<PetBehaviorProfile> PetBehaviorProfiles { get; set; }
    public DbSet<FeedbackMaster> FeedbackMasters { get; set; }
    public DbSet<NotificationMaster> NotificationMasters { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<CardPayment> CardPayments { get; set; }
    public DbSet<WhatsAppPayment> WhatsAppPayments { get; set; }
}
