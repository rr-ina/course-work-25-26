using Cinema.API.Data;
using Cinema.API.Models;
using Cinema.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Repositories.Implementations
{
    public class HallRepo : IHallRepo
    {
        private readonly CinemaDbContext _context;
        public HallRepo(CinemaDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Hall hall)
        {
            await _context.Halls.AddAsync(hall);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hall = await _context.Halls.FindAsync(id);
            if (hall != null)
            {
                _context.Halls.Remove(hall);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Hall>> GetAllAsync()
        {
            return await _context.Halls.ToListAsync();
        }

        public async Task<Hall?> GetByIdAsync(int id)
        {
            return await _context.Halls.FindAsync(id);
        }
    }
}