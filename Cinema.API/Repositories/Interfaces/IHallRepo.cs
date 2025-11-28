using Cinema.API.Models;

namespace Cinema.API.Repositories.Interfaces
{
    public interface IHallRepo
    { 
        Task<IEnumerable<Hall>> GetAllAsync();
        Task<Hall?> GetByIdAsync(int id);
        Task AddAsync(Hall hall);
        Task DeleteAsync(int id);
    }
}