using BloodBankAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("api/admin/donors")]
    [Authorize(Roles = "Admin")]
    public class AdminDonorsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AdminDonorsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDonors()
        {
            var donors = await _db.Users
                .Select(u => new
                {
                    id = u.Id,
                    name = u.FullName,
                    donorId = _db.Donors
                        .Where(d => d.UserId == u.Id)
                        .Select(d => d.Id)
                        .FirstOrDefault()
                })
                .Where(x => x.donorId != Guid.Empty)
                .ToListAsync();

            return Ok(donors);
        }
    }

}
