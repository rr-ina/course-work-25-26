using Cinema.API.Models;

namespace Cinema.API.Repositories.Interfaces
{
    public interface IMovieRepo
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(int id);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(int id);
    }
}