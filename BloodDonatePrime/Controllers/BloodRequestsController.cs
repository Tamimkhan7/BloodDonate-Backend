using BloodBankAPI.Data;
using BloodBankAPI.DTOs;
using BloodBankAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("api/blood-requests")]
    public class BloodRequestsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly EmailService _email;

        public BloodRequestsController(AppDbContext context, EmailService email)
        {
            _context = context;
            _email = email;
        }

        // ADMIN → Reply to request

        [Authorize(Roles = "Admin")]
        [HttpPut("admin/{id}")]
        public async Task<IActionResult> Reply(Guid id, AdminBloodRequestReplyDto dto)
        {
            var req = await _context.BloodRequests
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (req == null) return NotFound("Blood request not found.");

            req.Status = dto.Status;
            req.AdminReply = dto.AdminReply;

            await _context.SaveChangesAsync();

            // 📩 Send email
            if (!string.IsNullOrEmpty(req.User.Email))
            {
                _email.Send(
                    req.User.Email,
                    $"Blood Request {dto.Status}",
                    $@"
                    <h3>Your blood request has been {dto.Status}</h3>
                    <p><b>Blood Group:</b> {req.BloodGroup}</p>
                    <p><b>Bags:</b> {req.Bags}</p>
                    <p><b>Admin Message:</b> {dto.AdminReply}</p>
                    "
                );
            }

            return Ok(req);
        }


        // Helper → Get current user Id

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
            var data = _context.BloodRequests
                .Include(x => x.User)
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return Ok(data);
        }
    }
}
