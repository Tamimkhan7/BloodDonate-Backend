namespace BloodBankAPI.DTOs
{
    public class DonorDto
    {
        public string? BloodGroup { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? LastDonationDate { get; set; }
        public string? MedicalHistory { get; set; }
        public double? Platilate { get; set; }
        public double? Hemoglobin { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? Temperature { get; set; }
        public double? BloodPressure { get; set; }
        public double? PulseRate { get; set; }
        public double? RespirationRate { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
