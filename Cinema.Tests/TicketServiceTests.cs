using Xunit;
using Moq;
using Cinema.API.Services;
using Cinema.API.Models;
using Cinema.API.Repositories.Interfaces;

namespace Cinema.Tests
{
    public class TicketServiceTests
    {
        private readonly Mock<ITiketRepo> _mockTicketRepo;
        private readonly Mock<ISessionRepo> _mockSessionRepo;

        private readonly TicketService _service;

        public TicketServiceTests()
        {
            _mockTicketRepo = new Mock<ITiketRepo>();
            _mockSessionRepo = new Mock<ISessionRepo>();

            _service = new TicketService(_mockTicketRepo.Object, _mockSessionRepo.Object);
        }

        [Fact]
        public async Task Purchase_Success()
        {
            //Arrange
            var session = new Session
            {
                Id = 1,
                StartTime = DateTime.UtcNow.AddHours(1),
                Hall = new Hall { Capacity = 50 },
            };
            var ticket = new Ticket { SessionId = 1, SeatNumber = 5 };

            _mockSessionRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(session);
            _mockTicketRepo.Setup(r => r.IsSeatOccupiedAsync(1, 5)).ReturnsAsync(false);

            //Act
            var result = await _service.BuyTicketAsync(ticket);

            //Assert
            Assert.NotNull(result);
            _mockTicketRepo.Verify(r => r.AddAsync(ticket), Times.Once);
        }

        [Fact]
        public async Task Purchase_SessionStarted_Error()
        {
            //Arrange
            var session = new Session
            {
                Id = 1,
                StartTime = DateTime.UtcNow.AddDays(-1),
                Hall = new Hall { Capacity = 50 },
            };
            var ticket = new Ticket { SessionId = 1, SeatNumber = 5 };

            _mockSessionRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(session);

            //Act+Assert
            await Assert.ThrowsAsync<Exception>(() => _service.BuyTicketAsync(ticket));
        }

        [Fact]
        public async Task Purchase_SeatOccupied_Error()
        {
            //Arrange
            var session = new Session
            {
                Id = 1,
                StartTime = DateTime.UtcNow.AddHours(1),
                Hall = new Hall { Capacity = 50 },
            };
            var ticket = new Ticket { SessionId = 1, SeatNumber = 5 };

            _mockSessionRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(session);

            _mockTicketRepo.Setup(r => r.IsSeatOccupiedAsync(1, 5)).ReturnsAsync(true);

            //Act+Assert
            await Assert.ThrowsAsync<Exception>(() => _service.BuyTicketAsync(ticket));
        }
    }
}