using Cinema.API.Models;

namespace Cinema.API.Services.Interfaces
{
    public interface ITicketService
    {
        Task<Ticket> BuyTicketAsync(Ticket ticket);
        Task<Ticket?> GetTicketByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetTicketsBySessionAsync(int sessionId);
    }
}