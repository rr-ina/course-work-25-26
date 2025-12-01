using Cinema.API.Models;
using Cinema.API.Repositories.Interfaces;
using Cinema.API.Services.Interfaces;

namespace Cinema.API.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITiketRepo _ticketRepo;
        private readonly ISessionRepo _sessionRepo;

        public TicketService(ITiketRepo ticketRepo, ISessionRepo sessionRepo)
        {
            _ticketRepo = ticketRepo;
            _sessionRepo = sessionRepo;
        }

        public async Task<Ticket> BuyTicketAsync(Ticket ticket)
        {
            var session = await _sessionRepo.GetByIdAsync(ticket.SessionId);

            if (session == null)
            {
                throw new Exception($"Session with ID {ticket.SessionId} not found.");
            }

            if (session.StartTime <= DateTime.UtcNow)
            {
                throw new Exception("Sales are closed. Session has already started.");
            }

            if (session.Hall != null && ticket.SeatNumber > session.Hall.Capacity)
            {
                throw new Exception($"Seat {ticket.SeatNumber} does not exist. Max capacity: {session.Hall.Capacity}");
            }

            if (ticket.SeatNumber <= 0)
            {
                throw new Exception("Seat number must be greater than 0.");
            }

            bool isOccupied = await _ticketRepo.IsSeatOccupiedAsync(ticket.SessionId, ticket.SeatNumber);
            if (isOccupied)
            {
                throw new Exception($"Seat {ticket.SeatNumber} is already taken.");
            }

            ticket.PurchaseTime = DateTime.UtcNow;

            await _ticketRepo.AddAsync(ticket);
            return ticket;
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            return await _ticketRepo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsBySessionAsync(int sessionId)
        {
            return await _ticketRepo.GetBySessionIdAsync(sessionId);
        }
    }
}
