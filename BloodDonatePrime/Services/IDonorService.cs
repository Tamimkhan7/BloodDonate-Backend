using BloodBankAPI.DTOs;
using BloodBankAPI.Models;

namespace BloodBankAPI.Services
{
    public interface IDonorService
    {
        Task<Donor> CreateOrUpdateDonorAsync(Guid userId, DonorDto dto);
        Task<Donor?> GetByUserIdAsync(Guid userId);
        Task<SearchResult> SearchDonorsAsync(
            string? bloodGroup,
            string? district,
            int page = 1,
            int pageSize = 10);
        Task<int> GetSearchCountAsync(
            string? bloodGroup,
            string? district);
        Task UpdateLocationAsync(Guid donorId, double lat, double lon);
        Task<List<string>> GetAvailableDistrictsAsync();

        Task SaveAsync();
    }


}