namespace BloodBankAPI.DTOs
{
    public class DonationHistoryDto
    {
        public DateTime Date { get; set; }
        public string? Location { get; set; }
        public double Amount { get; set; }
    }
}
