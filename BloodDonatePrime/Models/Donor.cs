using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankAPI.Models
{
    public class Donor
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        // Basic info
        public string? BloodGroup { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? NationalIdNumber { get; set; }

        // Address info
        public string? PresentAddress { get; set; }
        public string? PresentPoliceStation { get; set; }
        public string? PresentDistrict { get; set; }
        public string? PermanentAddress { get; set; }
        public string? PermanentPoliceStation { get; set; }
        public string? PermanentDistrict { get; set; }

        // Location
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // Donation info
        public DateTime? LastDonationDate { get; set; }
        public bool IsAvailable { get; set; } = true;

        // Profile photo
        public string? PhotoUrl { get; set; }

        // Medical info
        public string? MedicalInfo { get; set; }
    }
}
