using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankAPI.Models
{
    public class BloodRequest
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public string BloodGroup { get; set; } = null!;

        [Required]
        public int Bags { get; set; }

        [Required]
        public DateTime NeededDate { get; set; }

        public string? ExtraContact { get; set; }
        public string? Reason { get; set; }

        public string Status { get; set; } = "Pending"; // Pending | Approved | Rejected
        public string? AdminReply { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
