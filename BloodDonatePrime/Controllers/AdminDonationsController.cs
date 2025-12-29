using BloodBankAPI.Data;
using BloodBankAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("api/admin/donations")]
    [Authorize(Roles = "Admin")]
    public class AdminDonationsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AdminDonationsController(AppDbContext db)
        {
            _db = db;
        }

        //  Get donation history by DonorId
        [HttpGet("{donorId}")]
        public async Task<IActionResult> GetHistory(Guid donorId)
        {
            var data = await _db.DonationHistories
                .Where(d => d.DonorId == donorId)
                .OrderByDescending(d => d.Date)
                .ToListAsync();

            return Ok(data);
        }

        //  Update donation
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DonationHistoryDto dto)
        {
            var donation = await _db.DonationHistories.FindAsync(id);
            if (donation == null) return NotFound();

            donation.Date = dto.Date;
            donation.Location = dto.Location;
            donation.Amount = dto.Amount;

            await _db.SaveChangesAsync();
            return Ok(donation);
        }

        //  Delete donation
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var donation = await _db.DonationHistories.FindAsync(id);
            if (donation == null) return NotFound();

            _db.DonationHistories.Remove(donation);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
