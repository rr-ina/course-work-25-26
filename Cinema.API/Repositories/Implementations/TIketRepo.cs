using Cinema.API.Data;
using Cinema.API.Models;
using Cinema.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Repositories.Implementations
{
    public class TiketRepo : ITiketRepo
    {
        private readonly CinemaDbContext _context;
        public TiketRepo(CinemaDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
           return await _context.Tickets.Include(t => t.Session).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Ticket>> GetBySessionIdAsync(int sessionId)
        {
            return await _context.Tickets.Where(t => t.SessionId == sessionId).ToListAsync();
        }

        public async Task<bool> IsSeatOccupiedAsync(int sessionId, int seatNumber)
        {
            return await _context.Tickets
                .AnyAsync(t => t.SessionId == sessionId && t.SeatNumber == seatNumber);
        }
    }
}