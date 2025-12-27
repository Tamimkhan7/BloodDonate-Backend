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



        [HttpPost("me/photo")]
        [Authorize]
        public async Task<IActionResult> UploadPhoto([FromForm] IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
                return BadRequest("No photo uploaded");

            //  Validation
            var allowedTypes = new[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(photo.FileName).ToLower();

            if (!allowedTypes.Contains(ext))
                return BadRequest("Only JPG, JPEG, PNG allowed");

            if (photo.Length > 2 * 1024 * 1024)
                return BadRequest("Max image size is 2MB");

            var donor = await _donorService.GetByUserIdAsync(GetUserId());
            if (donor == null) return NotFound("Donor not found");

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // Delete old photo
            if (!string.IsNullOrEmpty(donor.PhotoUrl))
            {
                var oldPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    donor.PhotoUrl.TrimStart('/')
                );

                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            donor.PhotoUrl = "/uploads/" + fileName;
            await _donorService.SaveAsync();

            return Ok(new { photoUrl = donor.PhotoUrl });
        }



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