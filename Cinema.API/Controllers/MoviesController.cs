using Microsoft.AspNetCore.Mvc;
using Cinema.API.Models;
using Cinema.API.Repositories.Interfaces;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepo _movieRepo;

        public MoviesController(IMovieRepo movieRepo)
        {
            _movieRepo = movieRepo;
        }

        /// <summary>
        /// Returns a list of all movies
        /// </summary>
        /// <returns>List of movies</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet(Name = "GetAllMovies")]
        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _movieRepo.GetAllAsync();
        }

        /// <summary>
        /// Returns a specific movie by ID
        /// </summary>
        /// <param name="id">Movie ID</param>
        /// <returns>The requested movie</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{id}", Name = "GetMovieById")]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            var movie = await _movieRepo.GetByIdAsync(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        /// <summary>
        /// Creates a new movie
        /// </summary>
        /// <param name="movie">Movie object</param>
        /// <returns>Created movie</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [HttpPost(Name = "CreateMovie")]
        public async Task<ActionResult<Movie>> Create(Movie movie)
        {
            if (string.IsNullOrEmpty(movie.Title))
                return BadRequest("Title is required");

            await _movieRepo.AddAsync(movie);
            return CreatedAtRoute("GetMovieById", new { id = movie.Id }, movie);
        }

        /// <summary>
        /// Updates an existing movie
        /// </summary>
        /// <param name="movie">Updated movie object</param>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [HttpPut(Name = "UpdateMovie")]
        public async Task<IActionResult> Update(Movie movie)
        {
            await _movieRepo.UpdateAsync(movie);
            return NoContent();
        }

        /// <summary>
        /// Deletes a movie by ID
        /// </summary>
        /// <param name="id">Movie ID</param>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        [HttpDelete("{id}", Name = "DeleteMovie")]
        public async Task<IActionResult> Delete(int id)
        {
            await _movieRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
