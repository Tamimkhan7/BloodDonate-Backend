namespace BloodBankAPI.Models
{
    public class DonorWithDistance
    {
        public Donor Donor { get; set; } = null!;
        public double? Distance { get; set; }
    }
}
