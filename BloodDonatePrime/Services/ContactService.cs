using BloodBankAPI.Data;
using BloodBankAPI.DTOs;
using BloodBankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodBankAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _db;
        public ContactService(AppDbContext db) { _db = db; }

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
    }
}
