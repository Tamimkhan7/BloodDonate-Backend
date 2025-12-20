namespace BloodBankAPI.DTOs
{
    public class AdminUpdateDonorDto
    {
        // ===== BASIC INFO =====
        public string? BloodGroup { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? NationalIdNumber { get; set; }

        // ===== ADDRESS =====
        public string? PresentAddress { get; set; }
        public string? PresentPoliceStation { get; set; }
        public string? PresentDistrict { get; set; }
        public string? PermanentAddress { get; set; }
        public string? PermanentPoliceStation { get; set; }
        public string? PermanentDistrict { get; set; }

        // ===== LOCATION =====
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // ===== DONATION =====
        public DateTime? LastDonationDate { get; set; }
        public bool? IsAvailable { get; set; }

        // ===== PHOTO =====
        public string? PhotoUrl { get; set; }

        // ===== MEDICAL =====
        public string? MedicalInfo { get; set; }
    }
}
