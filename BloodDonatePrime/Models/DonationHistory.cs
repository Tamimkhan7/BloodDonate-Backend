namespace BloodBankAPI.Models
{
    public class DonationHistory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid DonorId { get; set; }
        public Donor? Donor { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }
    }
}
