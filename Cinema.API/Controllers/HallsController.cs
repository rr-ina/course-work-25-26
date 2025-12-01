using Microsoft.AspNetCore.Mvc;
using Cinema.API.Models;
using Cinema.API.Repositories.Interfaces;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HallsController : ControllerBase
    {
        private readonly IHallRepo _hallRepo;

        public HallsController(IHallRepo hallRepo)
        {
            _hallRepo = hallRepo;
        }

        /// <summary>
        /// Returns a list of all halls
        /// </summary>
        /// <returns>List of halls</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet(Name = "GetAllHalls")]
        public async Task<IEnumerable<Hall>> GetAll()
        {
            return await _hallRepo.GetAllAsync();
        }

        /// <summary>
        /// Returns a specific hall by ID
        /// </summary>
        /// <param name="id">Hall ID</param>
        /// <returns>The requested hall</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{id}", Name = "GetHallById")]
        public async Task<ActionResult<Hall>> GetById(int id)
        {
            var hall = await _hallRepo.GetByIdAsync(id);
            if (hall == null)
            {
                return NotFound();
            }
            return Ok(hall);
        }

        /// <summary>
        /// Creates a new hall
        /// </summary>
        /// <param name="hall">Hall object</param>
        /// <returns>Created hall</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [HttpPost(Name = "CreateHall")]
        public async Task<ActionResult<Hall>> Create(Hall hall)
        {
            if (string.IsNullOrEmpty(hall.Name))
                return BadRequest("Name is required");

            if (hall.Capacity <= 0)
                return BadRequest("Capacity must be greater than 0");

            await _hallRepo.AddAsync(hall);
            return CreatedAtRoute("GetHallById", new { id = hall.Id }, hall);
        }

        /// <summary>
        /// Deletes a hall by ID
        /// </summary>
        /// <param name="id">Hall ID</param>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        [HttpDelete("{id}", Name = "DeleteHall")]
        public async Task<IActionResult> Delete(int id)
        {
            await _hallRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
