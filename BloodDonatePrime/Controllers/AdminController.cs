using BloodBankAPI.Data;
using BloodBankAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AdminController(AppDbContext db) { _db = db; }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _db.Users
                .Include(u => u.DonorProfile)
                .ToListAsync();
            return Ok(users);
        }

        [HttpPost("toggleBan/{id}")]
        public async Task<IActionResult> ToggleBan(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();
            user.IsBanned = !user.IsBanned;
            await _db.SaveChangesAsync();
            return Ok(new { user.Id, user.IsBanned });
        }

        [HttpPut("donor/{userId}")]
        public async Task<IActionResult> UpdateDonor(Guid userId, AdminUpdateDonorDto dto)
        {
            var donor = await _db.Donors.FirstOrDefaultAsync(d => d.UserId == userId);
            if (donor == null) return NotFound("Donor not found");

            // ===== BASIC INFO =====
            donor.BloodGroup = dto.BloodGroup ?? donor.BloodGroup;
            donor.Age = dto.Age ?? donor.Age;
            donor.Gender = dto.Gender ?? donor.Gender;
            donor.Phone = dto.Phone ?? donor.Phone;
            donor.Email = dto.Email ?? donor.Email;
            donor.DateOfBirth = dto.DateOfBirth ?? donor.DateOfBirth;
            donor.NationalIdNumber = dto.NationalIdNumber ?? donor.NationalIdNumber;

            // ===== ADDRESS =====
            donor.PresentAddress = dto.PresentAddress ?? donor.PresentAddress;
            donor.PresentPoliceStation = dto.PresentPoliceStation ?? donor.PresentPoliceStation;
            donor.PresentDistrict = dto.PresentDistrict ?? donor.PresentDistrict;
            donor.PermanentAddress = dto.PermanentAddress ?? donor.PermanentAddress;
            donor.PermanentPoliceStation = dto.PermanentPoliceStation ?? donor.PermanentPoliceStation;
            donor.PermanentDistrict = dto.PermanentDistrict ?? donor.PermanentDistrict;

            // ===== LOCATION =====
            donor.Latitude = dto.Latitude ?? donor.Latitude;
            donor.Longitude = dto.Longitude ?? donor.Longitude;

            // ===== DONATION =====
            donor.LastDonationDate = dto.LastDonationDate ?? donor.LastDonationDate;
            donor.IsAvailable = dto.IsAvailable ?? donor.IsAvailable;

            // ===== PHOTO & MEDICAL =====
            donor.PhotoUrl = dto.PhotoUrl ?? donor.PhotoUrl;
            donor.MedicalInfo = dto.MedicalInfo ?? donor.MedicalInfo;

            await _db.SaveChangesAsync();
            return Ok(donor);
        }
    }
}
