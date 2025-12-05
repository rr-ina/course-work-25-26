using Microsoft.AspNetCore.Mvc;
using Cinema.API.Models;
using Cinema.API.Services.Interfaces;
using Cinema.API.Services;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Purchases a ticket
        /// </summary>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [HttpPost("purchase")]
        public async Task<ActionResult<Ticket>> Purchase(Ticket ticket)
        {
            try
            {
                var createdTicket = await _ticketService.BuyTicketAsync(ticket);
                return Ok(createdTicket);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get ticket details by ID
        /// </summary>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetById(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }

        /// <summary>
        /// Get all tickets for a specific session
        /// </summary>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("session/{sessionId}")]
        public async Task<IEnumerable<Ticket>> GetBySession(int sessionId)
        {
            return await _ticketService.GetTicketsBySessionAsync(sessionId);
        }
    }
}