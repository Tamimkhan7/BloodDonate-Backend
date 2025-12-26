using BloodBankAPI.Data;
using BloodBankAPI.DTOs;
using BloodBankAPI.Models;
using BloodBankAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly AppDbContext _context;
        private readonly EmailService _email;


        public AuthController(IAuthService auth, EmailService email, AppDbContext context)
        {
            _auth = auth;
            _email = email;
            _context = context;
        }

        //forget password 

        [HttpPost("forget-password")]
        public IActionResult ForgetPassword([FromBody] ForgetPasswordDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null) return NotFound("User not found");

            var token = Guid.NewGuid().ToString();

            var resetToken = new PasswordResetToken
            {
                UserId = user.Id,
                Token = token,
                ExpireAt = DateTime.UtcNow.AddHours(1)
            };

            _context.PasswordResetTokens.Add(resetToken);
            _context.SaveChanges();

            var resetLink = $"http://localhost:3000/reset-password?token={token}";

            _email.Send(user.Email, "BloodBank Password Reset", $@"
                <h3>Password Reset Request</h3>
                <p>Click the link below to reset your password (valid 1 hour):</p>
                <a href='{resetLink}'>Reset Password</a>
            ");

            return Ok("Password reset link sent to your email");
        }

        // 2️⃣ Reset Password → using token
        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var token = _context.PasswordResetTokens
                .FirstOrDefault(t => t.Token == dto.Token && !t.Used);

            if (token == null || token.ExpireAt < DateTime.UtcNow)
                return BadRequest("Invalid or expired token");

            var user = _context.Users.FirstOrDefault(u => u.Id == token.UserId);
            if (user == null) return NotFound("User not found");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            token.Used = true;

            _context.SaveChanges();

            return Ok("Password reset successfully");
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                var user = await _auth.RegisterAsync(dto);
                return Ok(new { user.Id, user.Email, user.FullName });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var token = await _auth.LoginAsync(dto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
