using Microsoft.AspNetCore.Mvc;
using MyOnlineCv.Models;
using MyOnlineCv.Data;

namespace MyOnlineCv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST api/contact -> User submits their details
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ContactRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new { error = "Email and Message are required." });
            }

            _context.ContactRequests.Add(request);
            await _context.SaveChangesAsync();

            return Ok(new { status = "Success", message = "Your message has been submitted successfully!", data = request });
        }

        // GET api/contact -> View all user submissions
        [HttpGet]
        public IActionResult GetMessages()
        {
            var messages = _context.ContactRequests.ToList();
            return Ok(messages);
        }
    }
}