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
using BBMS.Services.Interfaces;

namespace BBMS.Tests
{
    public class BreweryControllerTests
    {
        private readonly Mock<IBreweryService> _mockBreweryService;
        private readonly BreweryController _controller;

        public BreweryControllerTests()
        {
            _mockBreweryService = new Mock<IBreweryService>();
            _controller = new BreweryController(_mockBreweryService.Object);
        }


        [Fact]
        public async Task GetAllBrewerys_ReturnsOkResult_WithBrewerys()
        {
            // Arrange
            var expectedBrewerys = new List<Brewery> { new Brewery { Id = 1, Name = "Brewery 1" }, new Brewery { Id = 2, Name = "Brewery 2" } };
            _mockBreweryService.Setup(service => service.GetAllBrewerysAsync()).ReturnsAsync(expectedBrewerys);

            // Act
            var result = await _controller.GetAllBrewerys();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var brewerys = Assert.IsAssignableFrom<IEnumerable<Brewery>>(okResult.Value);
            Assert.Equal(expectedBrewerys.Count, brewerys.Count());

            _mockBreweryService.Verify(service => service.GetAllBrewerysAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllBrewerys_ReturnsNotFound_WhenBrewerysAreNull()
        {
            // Arrange
            _mockBreweryService.Setup(service => service.GetAllBrewerysAsync()).ReturnsAsync((IEnumerable<Brewery>)null);

            // Act
            var result = await _controller.GetAllBrewerys();

            // Assert
            Assert.IsType<NotFoundResult>(result);

            _mockBreweryService.Verify(service => service.GetAllBrewerysAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllBrewerys_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            _mockBreweryService.Setup(service => service.GetAllBrewerysAsync()).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _controller.GetAllBrewerys();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);

            _mockBreweryService.Verify(service => service.GetAllBrewerysAsync(), Times.Once);
        }

        [Fact]
        public async Task GetBreweryById_ReturnsOkResult_WithBrewery()
        {
            // Arrange
            var id = 1;
            var expectedBrewery = new Brewery { Id = id, Name = "Brewery 1" };
            _mockBreweryService.Setup(service => service.GetBreweryByIdAsync(id)).ReturnsAsync(expectedBrewery);

            // Act
            var result = await _controller.GetBreweryById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var brewery = Assert.IsType<Brewery>(okResult.Value);
            Assert.Equal(expectedBrewery.Id, brewery.Id);
            Assert.Equal(expectedBrewery.Name, brewery.Name);

            _mockBreweryService.Verify(service => service.GetBreweryByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetBreweryById_ReturnsNotFound_WhenBreweryIsNull()
        {
            // Arrange
            var id = 1;
            _mockBreweryService.Setup(service => service.GetBreweryByIdAsync(id)).ReturnsAsync((Brewery)null);

            // Act
            var result = await _controller.GetBreweryById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);

            _mockBreweryService.Verify(service => service.GetBreweryByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetBreweryById_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var id = 1;
            _mockBreweryService.Setup(service => service.GetBreweryByIdAsync(id)).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _controller.GetBreweryById(id);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);

            _mockBreweryService.Verify(service => service.GetBreweryByIdAsync(id), Times.Once);
        }

      

        [Fact]
        public async Task PostBrewery_ReturnsBadRequest_WhenBreweryIsNull()
        {
            // Arrange

            // Act
            var result = await _controller.PostBrewery(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

            _mockBreweryService.Verify(service => service.CreateBreweryAsync(It.IsAny<Brewery>()), Times.Never);
        }

        [Fact]
        public async Task PostBrewery_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var brewery = new Brewery { Name = "Brewery 1" };
            _mockBreweryService.Setup(service => service.CreateBreweryAsync(brewery)).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _controller.PostBrewery(brewery);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);

            _mockBreweryService.Verify(service => service.CreateBreweryAsync(brewery), Times.Once);
        }

        [Fact]
        public async Task UpdateBrewery_ReturnsOkResult_WhenBreweryExists()
        {
            // Arrange
            var id = 1;
            var updatedBrewery = new Brewery { Id = id, Name = "Updated Brewery" };
            var existingBrewery = new Brewery { Id = id, Name = "Existing Brewery" };

            _mockBreweryService.Setup(service => service.GetBreweryByIdAsync(id)).ReturnsAsync(existingBrewery);
            

            // Act
            var result = await _controller.UpdateBrewery(id, updatedBrewery);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBrewery = Assert.IsType<Brewery>(okResult.Value);
            Assert.Equal(updatedBrewery.Id, returnedBrewery.Id);
            Assert.Equal(updatedBrewery.Name, returnedBrewery.Name);

            _mockBreweryService.Verify(service => service.GetBreweryByIdAsync(id), Times.Once);
            _mockBreweryService.Verify(service => service.UpdateBreweryAsync(existingBrewery), Times.Once);
        }

        [Fact]
        public async Task UpdateBrewery_ReturnsNotFound_WhenBreweryDoesNotExist()
        {
            // Arrange
            var id = 1;
            var updatedBrewery = new Brewery { Id = id, Name = "Updated Brewery" };

            _mockBreweryService.Setup(service => service.GetBreweryByIdAsync(id));

            // Act
            var result = await _controller.UpdateBrewery(id, updatedBrewery);

            // Assert
            Assert.IsType<NotFoundResult>(result);

            _mockBreweryService.Verify(service => service.GetBreweryByIdAsync(id), Times.Once);
            _mockBreweryService.Verify(service => service.UpdateBreweryAsync(It.IsAny<Brewery>()), Times.Never);
        }

       

      

    }
}
