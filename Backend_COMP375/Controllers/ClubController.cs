using Backend_COMP375.Data;
using Microsoft.AspNetCore.Mvc;
using Backend_COMP375.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Backend_COMP375.DTO;

namespace Backend_COMP375.Controllers
{
    [Route("api/clubs")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ClubController> _logger;

        public ClubController(AppDbContext context, ILogger<ClubController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubDTO>>> GetClubs()
        {
            var clubs = await _context.Clubs
                .Select(c => new ClubDTO
                {
                    ClubId = c.ClubId,
                    Name = c.Name,                                               
                    Quantity = c.Quantity
                })
                .ToListAsync();

            return Ok(clubs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Club>> GetClub(int id)
        {
            var club = await _context.Clubs.FindAsync(id);

            if (club == null)
            {
                _logger.LogWarning("Club item with ID {ClubId} not found.", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully fetched club item {ClubId}.", id);
            return club;
        }
    }
}
