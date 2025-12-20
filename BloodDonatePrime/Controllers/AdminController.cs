using BloodBankAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Only Admin can access
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AdminController(AppDbContext db) { _db = db; }

        // GET: api/Admin/users
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _db.Users
                                 .Include(u => u.DonorProfile)
                                 .ToListAsync();
            return Ok(users);
        }

        // POST: api/Admin/toggleBan/{id}
        [HttpPost("toggleBan/{id}")]
        public async Task<IActionResult> ToggleBan(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.IsBanned = !user.IsBanned;
            await _db.SaveChangesAsync();

            return Ok(new { user.Id, user.IsBanned });
        }
    }
}
