using ContactApi.Models;
using ContactApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly ContactService _service;

    public ContactController(ContactService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ContactMessage message)
    {
        if (string.IsNullOrEmpty(message.Email) || string.IsNullOrEmpty(message.Message))
            return BadRequest("Email and message are required");

        await _service.CreateAsync(message);
        return Ok(new { message = "Submitted successfully" });
    }

    [HttpGet]
    public async Task<List<ContactMessage>> Get() => await _service.GetAsync();
}
