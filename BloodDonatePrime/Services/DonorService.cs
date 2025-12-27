using BloodBankAPI.Data;
using BloodBankAPI.DTOs;
using BloodBankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodBankAPI.Services
{
    public class DonorService : IDonorService
    {
        private readonly AppDbContext _db;
        public DonorService(AppDbContext db) { _db = db; }

        public async Task<Donor> CreateOrUpdateDonorAsync(Guid userId, DonorDto dto)
        {
            var donor = await _db.Donors.FirstOrDefaultAsync(d => d.UserId == userId);

            if (donor == null)
            {
                donor = new Donor { UserId = userId };
                _db.Donors.Add(donor);
            }

            donor.BloodGroup = dto.BloodGroup;
            donor.Age = dto.Age;
            donor.Gender = dto.Gender;
            donor.Phone = dto.Phone;
            donor.Email = dto.Email;
            donor.DateOfBirth = dto.DateOfBirth;
            donor.NationalIdNumber = dto.NationalIdNumber;
            donor.PresentAddress = dto.PresentAddress;
            donor.PresentDistrict = dto.PresentDistrict;
            donor.PermanentDistrict = dto.PermanentDistrict;
            donor.LastDonationDate = dto.LastDonationDate;
            donor.Latitude = dto.Latitude;
            donor.Longitude = dto.Longitude;
            donor.IsAvailable = dto.IsAvailable;
            donor.PhotoUrl = dto.PhotoUrl;
            donor.MedicalInfo = dto.MedicalInfo;

            await _db.SaveChangesAsync();
            return donor;
        }

        public async Task<Donor?> GetByUserIdAsync(Guid userId)
        {
            return await _db.Donors
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.UserId == userId);
        }

        public async Task<SearchResult> SearchDonorsAsync(
            string? bloodGroup,
            string? district,
            int page = 1,
            int pageSize = 10)
        {
            var q = _db.Donors
                .Include(d => d.User)
                .Where(d => d.IsAvailable)
                .AsQueryable();

            // Filter by blood group
            if (!string.IsNullOrEmpty(bloodGroup) && bloodGroup != "All")
                q = q.Where(d => d.BloodGroup == bloodGroup);

            // Filter by district
            if (!string.IsNullOrEmpty(district) && district != "All")
            {
                q = q.Where(d => d.PresentDistrict == district ||
                               d.PermanentDistrict == district);
            }

            var totalCount = await q.CountAsync();

            // Apply sorting by last donation date (most recent first)
            var donors = await q
                .OrderByDescending(d => d.LastDonationDate) // Most recent donors first
                .ThenByDescending(d => d.IsAvailable)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = donors.Select(d => new DonorWithDistance
            {
                Donor = d,
                Distance = null
            }).ToList();

            return new SearchResult
            {
                Donors = result,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<int> GetSearchCountAsync(
            string? bloodGroup,
            string? district)
        {
            var q = _db.Donors
                .Where(d => d.IsAvailable)
                .AsQueryable();

            if (!string.IsNullOrEmpty(bloodGroup) && bloodGroup != "All")
                q = q.Where(d => d.BloodGroup == bloodGroup);

            if (!string.IsNullOrEmpty(district) && district != "All")
            {
                q = q.Where(d => d.PresentDistrict == district ||
                               d.PermanentDistrict == district);
            }

            return await q.CountAsync();
        }

        public async Task UpdateLocationAsync(Guid donorId, double lat, double lon)
        {
            var donor = await _db.Donors.FindAsync(donorId);
            if (donor == null) throw new Exception("Donor not found");

            donor.Latitude = lat;
            donor.Longitude = lon;
            await _db.SaveChangesAsync();
        }

        public async Task<List<string>> GetAvailableDistrictsAsync()
        {
            // Return all 64 districts of Bangladesh
            return GetAllBangladeshDistricts();
        }

        private List<string> GetAllBangladeshDistricts()
        {
            return new List<string>
            {
                "Bagerhat", "Bandarban", "Barguna", "Barishal", "Bhola", "Bogra", "Brahmanbaria",
                "Chandpur", "Chattogram", "Chuadanga", "Comilla", "Cox's Bazar", "Dhaka",
                "Dinajpur", "Faridpur", "Feni", "Gaibandha", "Gazipur", "Gopalganj", "Habiganj",
                "Jamalpur", "Jashore", "Jhalokati", "Jhenaidah", "Joypurhat", "Khagrachhari",
                "Khulna", "Kishoreganj", "Kurigram", "Kushtia", "Lakshmipur", "Lalmonirhat",
                "Madaripur", "Magura", "Manikganj", "Meherpur", "Moulvibazar", "Munshiganj",
                "Mymensingh", "Naogaon", "Narail", "Narayanganj", "Narsingdi", "Natore",
                "Netrokona", "Nilphamari", "Noakhali", "Pabna", "Panchagarh", "Patuakhali",
                "Pirojpur", "Rajbari", "Rajshahi", "Rangamati", "Rangpur", "Satkhira",
                "Shariatpur", "Sherpur", "Sirajganj", "Sunamganj", "Sylhet", "Tangail",
                "Thakurgaon"
            };
        }

        private double DistanceInKm(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371;
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return Math.Round(R * c, 2);
        }

        private double ToRadians(double deg) => deg * (Math.PI / 180);


        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }

}