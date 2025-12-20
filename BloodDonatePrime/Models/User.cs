using System.ComponentModel.DataAnnotations;

namespace BloodBankAPI.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required] public string FullName { get; set; } = null!;
        [Required] public string Email { get; set; } = null!;
        [Required] public string PasswordHash { get; set; } = null!;
        public string? Phone { get; set; }
        public bool IsBanned { get; set; } = false;
        public string Role { get; set; } = "Donor";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Donor? DonorProfile { get; set; }
    }
}
