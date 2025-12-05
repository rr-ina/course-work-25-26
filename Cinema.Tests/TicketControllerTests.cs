using Xunit;
using Moq;
using Cinema.API.Controllers;
using Cinema.API.Models;
using Cinema.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cinema.Tests
{
    public class TicketControllerTests
    {
        private readonly Mock<ITicketService> _mockService;
        private readonly TicketController _controller;

        public TicketControllerTests()
        {
            _mockService = new Mock<ITicketService>();
            _controller = new TicketController(_mockService.Object);
        }

        [Fact]
        public async Task Purchase_ReturnsOk_Success()
        {
            //Arrange
            var ticket = new Ticket { SessionId = 1, SeatNumber = 5 };
            _mockService.Setup(s => s.BuyTicketAsync(ticket)).ReturnsAsync(ticket);

            //Act
            var result = await _controller.Purchase(ticket);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Purchase_ReturnsBadRequest_OnError()
        {
            //Arrange
            var ticket = new Ticket { SessionId = 1, SeatNumber = 5 };

            _mockService.Setup(s => s.BuyTicketAsync(ticket))
                        .ThrowsAsync(new Exception("Seat is already taken."));

            //Act
            var result = await _controller.Purchase(ticket);

            //Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result); 
            Assert.NotNull(badRequest.Value); 
        }
    }
}