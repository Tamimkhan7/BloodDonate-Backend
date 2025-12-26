namespace BloodBankAPI.Models
{
    public class ContactMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // For admin reply
        public string? AdminReply { get; set; }
        public DateTime? RepliedAt { get; set; }
    }
}
