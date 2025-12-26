using BloodBankAPI.DTOs;
using BloodBankAPI.Models;

namespace BloodBankAPI.Services
{
    public interface IContactService
    {
        Task<ContactMessage> CreateMessageAsync(ContactMessageDto dto);
        Task<IEnumerable<ContactMessage>> GetAllMessagesAsync();
        Task<ContactMessage?> ReplyAsync(Guid id, string reply);
    }
}
