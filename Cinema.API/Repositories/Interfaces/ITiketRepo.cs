using Cinema.API.Models;

namespace Cinema.API.Repositories.Interfaces
{
    public interface ITiketRepo
    { 
        Task<Ticket?> GetByIdAsync(int id);
        Task AddAsync(Ticket ticket);
        Task<bool> IsSeatOccupiedAsync(int sessionId, int seatNumber);
        Task<IEnumerable<Ticket>> GetBySessionIdAsync(int sessionId);
    }
}