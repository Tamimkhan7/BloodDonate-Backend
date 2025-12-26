namespace BloodBankAPI.DTOs
{
    public class AdminBloodRequestReplyDto
    {
        public string Status { get; set; } = null!; // Approved / Rejected
        public string? AdminReply { get; set; }
    }
}
