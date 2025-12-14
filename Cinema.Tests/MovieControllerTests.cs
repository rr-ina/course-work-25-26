using Xunit;
using Moq;
using Cinema.API.Controllers;
using Cinema.API.Models;
using Cinema.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Tests
{
    public class MoviesControllerTests
    {
        private readonly Mock<IMovieRepo> _mockRepo;
        private readonly MoviesController _controller;

        public MoviesControllerTests()
        {
            _mockRepo = new Mock<IMovieRepo>();
            _controller = new MoviesController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            //Arrange
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Dune 2" },
                new Movie { Id = 2, Title = "Barbie" }
            };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(movies);

            //Act
            var result = await _controller.GetAll();

            //Assert
            var returnedMovies = Assert.IsAssignableFrom<IEnumerable<Movie>>(result);
            Assert.Equal(2, returnedMovies.Count());
        }

        [Fact]
        public async Task GetById_ReturnsNotFound()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((Movie?)null);

            //Act
            var result = await _controller.GetById(999);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_InvalidData()
        {
            //Arrange
            var invalidMovie = new Movie { Title = "" };

            //Act
            var result = await _controller.Create(invalidMovie);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated_Success()
        {
            //Arrange
            var movie = new Movie { Id = 1, Title = "Inception" };

            //Act
            var result = await _controller.Create(movie);

            //Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
            Assert.Equal(201, createdResult.StatusCode);
        }
    }
}