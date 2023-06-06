using Moq;
using API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using Services.Interfaces;
using Domain.Models;
using static System.Collections.Specialized.BitVector32;
using Microsoft.AspNetCore.Http;
using Services;

namespace BBMS.Tests
{
    public class BeerControllerTests
    {
        private readonly Mock<IBeerService> _mockBeerService;
        private readonly BeerController _beerController;

        public BeerControllerTests()
        {
            _mockBeerService = new Mock<IBeerService>();
            _beerController = new BeerController(_mockBeerService.Object);
        }

        [Fact]
        public async Task GetAllBeers_ReturnsOkResult_WhenBeersExist()
        {
            // Arrange
            var expectedBeers = new List<Beer> { new Beer { Id = 1, Name = "IPA" }, new Beer { Id = 2, Name = "Stout" } };
            _mockBeerService.Setup(service => service.GetAllBeersAsync()).ReturnsAsync(expectedBeers);

            // Act
            var result = await _beerController.GetAllBeers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualBeers = Assert.IsAssignableFrom<IEnumerable<Beer>>(okResult.Value);
            Assert.Equal(expectedBeers.Count, actualBeers.Count());
        }

        [Fact]
        public async Task GetAllBeers_ReturnsNotFound_WhenNoBeersExist()
        {
            // Arrange
            _mockBeerService.Setup(service => service.GetAllBeersAsync()).ReturnsAsync((IEnumerable<Beer>)null);

            // Act
            var result = await _beerController.GetAllBeers();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllBeers_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            _mockBeerService.Setup(service => service.GetAllBeersAsync()).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _beerController.GetAllBeers();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }


        [Fact]
        public async Task GetBeerById_ReturnsOkResult_WhenBeerExists()
        {
            // Arrange
            int beerId = 1;
            var expectedBeer = new Beer { Id = beerId, Name = "IPA" };
            _mockBeerService.Setup(service => service.GetBeerByIdAsync(beerId)).ReturnsAsync(expectedBeer);

            // Act
            var result = await _beerController.GetBeerById(beerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualBeer = Assert.IsAssignableFrom<Beer>(okResult.Value);
            Assert.Equal(expectedBeer.Id, actualBeer.Id);
            Assert.Equal(expectedBeer.Name, actualBeer.Name);
        }

        [Fact]
        public async Task GetBeerById_ReturnsNotFound_WhenBeerDoesNotExist()
        {
            // Arrange
            int beerId = 1;
            _mockBeerService.Setup(service => service.GetBeerByIdAsync(beerId)).ReturnsAsync((Beer)null);

            // Act
            var result = await _beerController.GetBeerById(beerId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetBeerById_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            int beerId = 1;
            _mockBeerService.Setup(service => service.GetBeerByIdAsync(beerId)).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _beerController.GetBeerById(beerId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async Task PostBeer_ReturnsOkResult_WhenBeerIsValid()
        {
            // Arrange
            var beer = new Beer { Id = 1, Name = "IPA" };

            // Act
            var result = await _beerController.PostBeer(beer);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualBeer = Assert.IsAssignableFrom<Beer>(okResult.Value);
            Assert.Equal(beer.Id, actualBeer.Id);
            Assert.Equal(beer.Name, actualBeer.Name);

            _mockBeerService.Verify(service => service.CreateBeerAsync(beer), Times.Once);
        }

        [Fact]
        public async Task PostBeer_ReturnsBadRequest_WhenBeerIsNull()
        {
            // Act
            var result = await _beerController.PostBeer(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

            _mockBeerService.Verify(service => service.CreateBeerAsync(It.IsAny<Beer>()), Times.Never);
        }

        [Fact]
        public async Task PostBeer_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var beer = new Beer { Id = 1, Name = "IPA" };
            _mockBeerService.Setup(service => service.CreateBeerAsync(beer)).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _beerController.PostBeer(beer);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            _mockBeerService.Verify(service => service.CreateBeerAsync(beer), Times.Once);
        }




        [Fact]
        public async Task PostBeer_ReturnsInternalServerError_OnException()
        {
            // Arrange
            Beer beer = new Beer { Name = "New Beer" };
            _mockBeerService.Setup(service => service.CreateBeerAsync(beer))
                .ThrowsAsync(new Exception("An error occurred."));

            // Act
            var result = await _beerController.PostBeer(beer);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);

        }

        [Fact]
        public async Task UpdateBeer_ReturnsOkResult_WhenBeerExists()
        {
            // Arrange
            int beerId = 1;
            Beer existingBeer = new Beer { Id = beerId, Name = "Existing Beer" };
            Beer updatedBeer = new Beer { Id = beerId, Name = "Updated Beer" };

            _mockBeerService.Setup(service => service.GetBeerByIdAsync(beerId))
                .ReturnsAsync(existingBeer);

            // Act
            var result = await _beerController.UpdateBeer(beerId, updatedBeer);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateBeer_ReturnsNotFound_WhenBeerDoesNotExist()
        {
            // Arrange
            int beerId = 1;
            Beer updatedBeer = new Beer { Id = beerId, Name = "Updated Beer" };

            _mockBeerService.Setup(service => service.GetBeerByIdAsync(beerId))
                .ReturnsAsync((Beer)null);

            // Act
            var result = await _beerController.UpdateBeer(beerId, updatedBeer);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateBeer_ReturnsBadRequest_WhenBeerIsNull()
        {
            // Arrange
            int beerId = 1;
            Beer updatedBeer = null;

            // Act
            var result = await _beerController.UpdateBeer(beerId, updatedBeer);

            // Assert




            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task UpdateBar_ReturnsInternalServerError_OnException()
        {
            // Arrange
            int beerId = 1;
            Beer existingBeer = new Beer { Id = beerId, Name = "Existing Beer" };
            Beer updatedBeer = new Beer { Id = beerId, Name = "Updated Beer" };

            _mockBeerService.Setup(service => service.GetBeerByIdAsync(beerId))
                .ReturnsAsync(existingBeer);

            _mockBeerService.Setup(service => service.UpdateBeerAsync(existingBeer))
                .ThrowsAsync(new Exception("An error occurred."));

            // Act
            var result = await _beerController.UpdateBeer(beerId, updatedBeer);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }


    }
}
