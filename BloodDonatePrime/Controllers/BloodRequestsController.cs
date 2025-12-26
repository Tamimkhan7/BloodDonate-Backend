using BloodBankAPI.Data;
using BloodBankAPI.DTOs;
using BloodBankAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("api/blood-requests")]
    public class BloodRequestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BloodRequestsController(AppDbContext context)
        {
            _context = context;
        }

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // USER → Create request
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBloodRequestDto dto)
        {
            var request = new BloodRequest
            {
                UserId = GetUserId(),
                BloodGroup = dto.BloodGroup,
                Bags = dto.Bags,
                NeededDate = dto.NeededDate,
                ExtraContact = dto.ExtraContact,
                Reason = dto.Reason
            };

            _context.BloodRequests.Add(request);
            await _context.SaveChangesAsync();

            return Ok(request);
        }

        // USER → My requests
        [Authorize]
        [HttpGet("me")]
        public IActionResult MyRequests()
        {
            var userId = GetUserId();

            var data = _context.BloodRequests
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return Ok(data);
        }

        // ADMIN → All requests
        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult GetAll()
        {
            return Ok(_context.BloodRequests
                .OrderByDescending(x => x.CreatedAt)
                .ToList());
        }

        // ADMIN → Reply
        [Authorize(Roles = "Admin")]
        [HttpPut("admin/{id}")]
        public async Task<IActionResult> Reply(Guid id, AdminBloodRequestReplyDto dto)
        {
            var req = await _context.BloodRequests.FindAsync(id);
            if (req == null) return NotFound();

            req.Status = dto.Status;
            req.AdminReply = dto.AdminReply;

            await _context.SaveChangesAsync();
            return Ok(req);
        }
    }
}
