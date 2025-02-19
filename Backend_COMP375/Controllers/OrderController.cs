using Backend_COMP375.Data;
using Microsoft.AspNetCore.Mvc;
using Backend_COMP375.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Backend_COMP375.DTO;

namespace Backend_COMP375.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(AppDbContext context, ILogger<OrderController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrder()
        {
            var orders = await _context.Orders
                .Select(o => new OrderDTO
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    ClubId = o.ClubId,
                    OrderNumber = o.OrderNumber,
                    OrderDate = o.OrderDate
                })
                .ToListAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Club)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                _logger.LogWarning("Order item with ID {OrderId} not found.", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully fetched order item {OrderId}.", id);
            return order;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateUser(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserIid} created successfully.", order.UserId);

            return CreatedAtAction(nameof(GetOrder), new { id = order.UserId }, order);
        }
    }
}
