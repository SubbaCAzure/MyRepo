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
    public class BarControllerTests
    {

        private readonly Mock<IBarService> _mockBarService;
        private readonly BarController _controller;

        public BarControllerTests()
        {
            _mockBarService = new Mock<IBarService>();
            _controller = new BarController(_mockBarService.Object);
        }

        [Fact]
        public async Task GetAllBars_ReturnsNotFound_WhenBarsAreNull()
        {
            // Arrange
            List<Bar> expectedBars = null;
            _mockBarService.Setup(service => service.GetAllBarsAsync())
                .ReturnsAsync(expectedBars);

            // Act
            var result = await _controller.GetAllBars();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task GetAllBars_ReturnsOkResult_WithBars()
        {
            // Arrange
            List<Bar> expectedBars = new List<Bar>
            {
                new Bar { Id = 1, Name = "Bar 1" },
                new Bar { Id = 2, Name = "Bar 2" }
            };

            _mockBarService.Setup(service => service.GetAllBarsAsync())
                .ReturnsAsync(expectedBars);

            // Act
            var result = await _controller.GetAllBars();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var bars = Assert.IsAssignableFrom<IEnumerable<Bar>>(okResult.Value);
            Assert.Equal(expectedBars.Count, bars.Count());
        }

        [Fact]
        public async Task GetAllBars_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            _mockBarService.Setup(service => service.GetAllBarsAsync())
                .ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _controller.GetAllBars();

            // Assert
            //var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }


        [Fact]
        public async Task GetBarById_ReturnsOkResult_WithBar()
        {
            // Arrange
            int barId = 1;
            var expectedBar = new Bar { Id = barId, Name = "Bar 1" };
            

                _mockBarService.Setup(service => service.GetBarByIdAsync(barId))
                .ReturnsAsync(expectedBar);

            // Act
            var result = await _controller.GetBarById(barId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualBar = Assert.IsType<Bar>(okResult.Value);
            Assert.Equal(expectedBar.Id, actualBar.Id);
            Assert.Equal(expectedBar.Name, actualBar.Name);
        }

        [Fact]
        public async Task GetBarById_ReturnsNotFound_WhenBarDoesNotExist()
        {
            // Arrange
            int barId = 1;
            _mockBarService.Setup(service => service.GetBarByIdAsync(barId))
                .ReturnsAsync(null as Bar);

            // Act
            var result = await _controller.GetBarById(barId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetBarById_ReturnsBar_WhenBarExists()
        {
            // Arrange
            int barId = 1;
            Bar bar = new Bar { Id = barId, Name = "Sample Bar" };
            _mockBarService.Setup(service => service.GetBarByIdAsync(barId))
                .ReturnsAsync(bar);

            // Act
            var result = await _controller.GetBarById(barId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBar = Assert.IsType<Bar>(okResult.Value);
            Assert.Equal(barId, returnedBar.Id);
            Assert.Equal("Sample Bar", returnedBar.Name);
        }
 

        [Fact]
        public async Task GetBarById_ReturnsInternalServerError_OnException()
        {
            // Arrange
            int barId = 1;
            _mockBarService.Setup(service => service.GetBarByIdAsync(barId))
                .ThrowsAsync(new Exception("An error occurred."));

            // Act
            var result = await _controller.GetBarById(barId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

        }



        [Fact]
        public async Task PostBar_ReturnsOkResult_WhenBarIsValid()
        {
            // Arrange
            Bar bar = new Bar { Name = "New Bar" };

            // Act
            var result = await _controller.PostBar(bar);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBar = Assert.IsType<Bar>(okResult.Value);
            Assert.Equal(bar.Name, returnedBar.Name);
        }

        [Fact]
        public async Task PostBar_ReturnsBadRequest_WhenBarIsNull()
        {
            // Arrange
            Bar? bar = null;

            // Act
            var result = await _controller.PostBar(bar);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid bar data", badRequestResult.Value);
        }

        [Fact]
        public async Task PostBar_ReturnsInternalServerError_OnException()
        {
            // Arrange
            Bar bar = new Bar { Name = "New Bar" };
            _mockBarService.Setup(service => service.CreateBarAsync(bar))
                .ThrowsAsync(new Exception("An error occurred."));

            // Act
            var result = await _controller.PostBar(bar);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);

        }

        [Fact]
        public async Task UpdateBar_ReturnsOkResult_WhenBarExists()
        {
            // Arrange
            int barId = 1;
            Bar existingBar = new Bar { Id = barId, Name = "Existing Bar" };
            Bar updatedBar = new Bar { Id = barId, Name = "Updated Bar" };

            _mockBarService.Setup(service => service.GetBarByIdAsync(barId))
                .ReturnsAsync(existingBar);

            // Act
            var result = await _controller.UpdateBar(barId, updatedBar);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateBar_ReturnsNotFound_WhenBarDoesNotExist()
        {
            // Arrange
            int barId = 1;
            Bar updatedBar = new Bar { Id = barId, Name = "Updated Bar" };

            _mockBarService.Setup(service => service.GetBarByIdAsync(barId))
                .ReturnsAsync((Bar)null);

            // Act
            var result = await _controller.UpdateBar(barId, updatedBar);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateBar_ReturnsBadRequest_WhenBarIsNull()
        {
            // Arrange
            int barId = 1;
            Bar updatedBar = null;

            // Act
            var result = await _controller.UpdateBar(barId, updatedBar);

            // Assert




            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task UpdateBar_ReturnsInternalServerError_OnException()
        {
            // Arrange
            int barId = 1;
            Bar existingBar = new Bar { Id = barId, Name = "Existing Bar" };
            Bar updatedBar = new Bar { Id = barId, Name = "Updated Bar" };

            _mockBarService.Setup(service => service.GetBarByIdAsync(barId))
                .ReturnsAsync(existingBar);

            _mockBarService.Setup(service => service.UpdateBarAsync(existingBar))
                .ThrowsAsync(new Exception("An error occurred."));

            // Act
            var result = await _controller.UpdateBar(barId, updatedBar);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }


    }
}
