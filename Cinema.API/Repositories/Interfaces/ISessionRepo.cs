using Cinema.API.Models;

namespace Cinema.API.Repositories.Interfaces
{
    public interface ISessionRepo
    { 
        Task<IEnumerable<Session>> GetAllAsync();
        Task<Session?> GetByIdAsync(int id);
        Task AddAsync(Session session);
        Task DeleteAsync(int id);
    }
}