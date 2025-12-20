using BloodBankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodBankAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Donor> Donors => Set<Donor>();
        public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
        //DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<DonationHistory> DonationHistories => Set<DonationHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.DonorProfile)
                .WithOne(d => d.User)
                .HasForeignKey<Donor>(d => d.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
