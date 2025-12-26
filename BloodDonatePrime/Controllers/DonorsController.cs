using BloodBankAPI.DTOs;
using BloodBankAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonorsController : ControllerBase
    {
        private readonly IDonorService _donorService;
        public DonorsController(IDonorService donorService) { _donorService = donorService; }

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost("me")]
        [Authorize]
        public async Task<IActionResult> CreateOrUpdateMe([FromBody] DonorDto dto)
        {
            var donor = await _donorService.CreateOrUpdateDonorAsync(GetUserId(), dto);
            return Ok(donor);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var donor = await _donorService.GetByUserIdAsync(GetUserId());
            if (donor == null) return Ok(null);
            return Ok(donor);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(
            [FromQuery] string? bloodGroup,
            [FromQuery] string? district,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _donorService.SearchDonorsAsync(
                bloodGroup, district, page, pageSize);

            return Ok(result);
        }

        [HttpGet("districts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDistricts()
        {
            var districts = await _donorService.GetAvailableDistrictsAsync();
            return Ok(districts);
        }
    }
}