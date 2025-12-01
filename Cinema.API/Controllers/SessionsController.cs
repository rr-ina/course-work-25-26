using Microsoft.AspNetCore.Mvc;
using Cinema.API.Models;
using Cinema.API.Repositories.Interfaces;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionRepo _sessionRepo;

        public SessionsController(ISessionRepo sessionRepo)
        {
            _sessionRepo = sessionRepo;
        }

        /// <summary>
        /// Returns a list of all sessions (schedule)
        /// </summary>
        /// <returns>List of sessions with movie and hall details</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet(Name = "GetAllSessions")]
        public async Task<IEnumerable<Session>> GetAll()
        {
            return await _sessionRepo.GetAllAsync();
        }

        /// <summary>
        /// Returns a specific session by ID
        /// </summary>
        /// <param name="id">Session ID</param>
        /// <returns>The requested session</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{id}", Name = "GetSessionById")]
        public async Task<ActionResult<Session>> GetById(int id)
        {
            var session = await _sessionRepo.GetByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }

        /// <summary>
        /// Creates a new session (adds to schedule)
        /// </summary>
        /// <param name="session">Session object</param>
        /// <returns>Created session</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [HttpPost(Name = "CreateSession")]
        public async Task<ActionResult<Session>> Create(Session session)
        {
            // Проста перевірка, щоб не передавали 0 або від'ємні ID
            if (session.MovieId <= 0 || session.HallId <= 0)
            {
                return BadRequest("MovieId and HallId are required and must be valid.");
            }

            // Перевірка ціни
            if (session.TicketPrice < 0)
            {
                return BadRequest("Price cannot be negative.");
            }

            await _sessionRepo.AddAsync(session);
            return CreatedAtRoute("GetSessionById", new { id = session.Id }, session);
        }

        /// <summary>
        /// Deletes a session by ID
        /// </summary>
        /// <param name="id">Session ID</param>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        [HttpDelete("{id}", Name = "DeleteSession")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sessionRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
