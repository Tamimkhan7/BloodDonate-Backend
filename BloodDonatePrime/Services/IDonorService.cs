using BloodBankAPI.DTOs;
using BloodBankAPI.Models;

namespace BloodBankAPI.Services
{
    public interface IDonorService
    {
        Task<Donor> CreateOrUpdateDonorAsync(Guid userId, DonorDto dto);

        Task<Donor?> GetByUserIdAsync(Guid userId); // ✅ ADD THIS

        Task<IEnumerable<Donor>> SearchDonorsAsync(string? bloodGroup, double? lat, double? lon);

        Task UpdateLocationAsync(Guid donorId, double lat, double lon);
    }
}
