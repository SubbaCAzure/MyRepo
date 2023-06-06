using API.Controllers;
using BBMS.API.Controllers;
using BBMS.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBMS.Tests
{
    public class BarBeersControllerTests
    {
        private readonly Mock<IBarBeerService> _mockBarBeersService;
        private readonly BarBeerController _controller;

        public BarBeersControllerTests()
        {
            //_mockBarBeersService = new Mock<IBarBeerService>();
            //_controller = new BarBeerController(_mockBarBeersService.Object);
        }

        

    }
 
}
