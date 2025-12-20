namespace BloodBankAPI.Models
{
    public class Donor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string? BloodGroup { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? LastDonationDate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int TotalDonations { get; set; } = 0;
    }
}
