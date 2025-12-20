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
            donor.Address = dto.Address;
            donor.Latitude = dto.Latitude;
            donor.Longitude = dto.Longitude;
            donor.IsAvailable = dto.IsAvailable;

            await _db.SaveChangesAsync();
            return donor;
        }

        // ✅ FIXED & USED
        public async Task<Donor?> GetByUserIdAsync(Guid userId)
        {
            return await _db.Donors
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.UserId == userId);
        }

        public async Task<IEnumerable<Donor>> SearchDonorsAsync(string? bloodGroup, double? lat, double? lon)
        {
            var q = _db.Donors.Include(d => d.User).AsQueryable();

            if (!string.IsNullOrEmpty(bloodGroup))
                q = q.Where(d => d.BloodGroup == bloodGroup);

            var list = await q.ToListAsync();

            if (lat.HasValue && lon.HasValue)
            {
                list = list
                    .OrderByDescending(d => d.IsAvailable)
                    .ThenBy(d => DistanceInKm(
                        lat.Value, lon.Value,
                        d.Latitude ?? 0, d.Longitude ?? 0))
                    .ToList();
            }
            else
            {
                list = list.OrderByDescending(d => d.IsAvailable).ToList();
            }

            return list;
        }

        public async Task UpdateLocationAsync(Guid donorId, double lat, double lon)
        {
            var donor = await _db.Donors.FindAsync(donorId);
            if (donor == null) throw new Exception("Donor not found");

            donor.Latitude = lat;
            donor.Longitude = lon;
            await _db.SaveChangesAsync();
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
            return R * c;
        }

        private double ToRadians(double deg) => deg * (Math.PI / 180);
    }
}
