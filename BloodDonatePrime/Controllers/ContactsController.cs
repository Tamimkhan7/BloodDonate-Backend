using BloodBankAPI.DTOs;
using BloodBankAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _service;
        public ContactsController(IContactService service) { _service = service; }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] ContactMessageDto dto)
        {
            var msg = await _service.CreateMessageAsync(dto);
            return Ok(msg);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllMessagesAsync();
            return Ok(list);
        }
    }
}
