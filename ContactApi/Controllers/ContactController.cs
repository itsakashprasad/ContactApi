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
        Console.WriteLine("POST /api/contact received:");
        Console.WriteLine($"Email: {message.Email}");
        Console.WriteLine($"Message: {message.Message}");

        if (string.IsNullOrEmpty(message.Email) || string.IsNullOrEmpty(message.Message))
        {
            Console.WriteLine("Validation failed: Missing Email or Message");
            return BadRequest("Email and message are required");
        }

        await _service.CreateAsync(message);
        Console.WriteLine("Contact saved successfully");
        return Ok(new { message = "Submitted successfully" });
    }

    [HttpGet]
    public async Task<List<ContactMessage>> Get()
    {
        Console.WriteLine("GET /api/contact called");
        var data = await _service.GetAsync();
        Console.WriteLine($"Fetched {data.Count} contact messages");
        return data;
    }
}
