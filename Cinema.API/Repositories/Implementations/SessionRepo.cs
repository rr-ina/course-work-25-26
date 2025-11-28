using Cinema.API.Data;
using Cinema.API.Models;
using Cinema.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Repositories.Implementations
{
    public class SessionRepo : ISessionRepo
    {
        private readonly CinemaDbContext _context;
        public SessionRepo(CinemaDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Session session)
        {
            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Session>> GetAllAsync()
        {
            return await _context.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .ToListAsync();
        }

        public async Task<Session?> GetByIdAsync(int id)
        {
            return await _context.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}