using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_COMP375.Models;
using Microsoft.Extensions.Logging;
using Backend_COMP375.Data;
using Backend_COMP375.DTO;
using System.Runtime.InteropServices;

namespace Backend_COMP375.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDbContext _context;

        public UserController(AppDbContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUser()
        {
            var users = await _context.Users
                .Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                _logger.LogWarning("User item with ID {UserId} not found.", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully fetched user item {UserId}.", id);
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserIid} created successfully.", user.UserId);

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }
    }
}
