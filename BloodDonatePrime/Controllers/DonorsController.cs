using BloodBankAPI.DTOs;
using BloodBankAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DonorsController : ControllerBase
    {
        private readonly IDonorService _donorService;
        public DonorsController(IDonorService donorService) { _donorService = donorService; }

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost("me")]
        public async Task<IActionResult> CreateOrUpdateMe([FromBody] DonorDto dto)
        {
            var donor = await _donorService.CreateOrUpdateDonorAsync(GetUserId(), dto);
            return Ok(donor);
        }

        // ✅ FIXED
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var donor = await _donorService.GetByUserIdAsync(GetUserId());
            if (donor == null) return NotFound();
            return Ok(donor);
        }


        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(
            [FromQuery] string? bloodGroup,
            [FromQuery] double? lat,
            [FromQuery] double? lon)
        {
            var donors = await _donorService.SearchDonorsAsync(bloodGroup, lat, lon);
            return Ok(donors);
        }
    }
}
