using BloodBankAPI.DTOs;
using BloodBankAPI.Models;

namespace BloodBankAPI.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}
