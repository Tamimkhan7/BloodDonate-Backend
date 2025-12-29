namespace BloodBankAPI.DTOs
{
    public class DonateBloodDto
    {
        public DateTime DonationDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}
