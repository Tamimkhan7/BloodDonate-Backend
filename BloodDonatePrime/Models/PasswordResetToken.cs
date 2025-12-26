namespace BloodBankAPI.Models
{
    public class PasswordResetToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public bool Used { get; set; } = false;
    }
}
