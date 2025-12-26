using BloodBankAPI.Data;
using BloodBankAPI.DTOs;
using BloodBankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodBankAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _db;
        private readonly EmailService _email;
        public ContactService(AppDbContext db, EmailService email)
        {
            _db = db;
            _email = email;
        }

        public async Task<ContactMessage> CreateMessageAsync(ContactMessageDto dto)
        {
            var msg = new ContactMessage
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Message = dto.Message
            };

            _db.ContactMessages.Add(msg);
            await _db.SaveChangesAsync();
            return msg;
        }

        public async Task<IEnumerable<ContactMessage>> GetAllMessagesAsync()
        {
            return await _db.ContactMessages
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<ContactMessage?> ReplyAsync(Guid id, string reply)
        {
            var msg = await _db.ContactMessages.FindAsync(id);
            if (msg == null) return null;

            msg.AdminReply = reply;
            msg.RepliedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            // Send email to user using existing EmailService
            var subject = "Reply to your contact message";
            var body = $"Hello {msg.Name},<br/><br/>" +
                       $"Admin replied to your message:<br/><br/>" +
                       $"<b>Your Message:</b> {msg.Message}<br/>" +
                       $"<b>Admin Reply:</b> {reply}<br/><br/>" +
                       $"Best regards,<br/>Blood Bank Team";

            _email.Send(msg.Email, subject, body);

            return msg;
        }

    }
}
