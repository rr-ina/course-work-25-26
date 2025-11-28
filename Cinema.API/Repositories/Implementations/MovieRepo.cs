using Cinema.API.Data;
using Cinema.API.Models;
using Cinema.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Repositories.Implementations
{
    public class MovieRepo : IMovieRepo
    {
        private readonly CinemaDbContext _context;
        public MovieRepo(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }
    }
}