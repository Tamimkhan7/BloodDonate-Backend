namespace BloodBankAPI.DTOs
{
    public class DonorDto
    {
        public string? BloodGroup { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? NationalIdNumber { get; set; }

        public string? PresentAddress { get; set; }
        public string? PresentPoliceStation { get; set; }
        public string? PresentDistrict { get; set; }
        public string? PermanentAddress { get; set; }
        public string? PermanentPoliceStation { get; set; }
        public string? PermanentDistrict { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public DateTime? LastDonationDate { get; set; }
        public bool IsAvailable { get; set; } = true;

        public string? PhotoUrl { get; set; }
        public string? MedicalInfo { get; set; }
    }
}
