using BBMS.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarController : ControllerBase
    {
        private readonly IBarService _barService;
        public BarController(IBarService barService)
        {
            _barService = barService;
        }

        [HttpPost("/bar")]
        public async Task<IActionResult> PostBar([FromBody] Bar bar)
        {
            try
            {
                // Validate the incoming bar object
                if (bar == null)
                {
                    return BadRequest("Invalid bar data");
                }
                // Add the bar to the repository or service
                await _barService.CreateBarAsync(bar);

                // Return a success response
                return Ok(bar);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate error response
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBar(int id, [FromBody] Bar updatedBar)
        {
            try
            {
                // Get the existing bar by id
                var existingBar = await _barService.GetBarByIdAsync(id);

                if (existingBar == null)
                {
                    return NotFound();
                }

                // Update the properties of the existing bar with the values from updatedBar
                existingBar.Name = updatedBar.Name;
                existingBar.Address = updatedBar.Address;
                // Update other properties as needed

                // Call the service or repository method to update the bar
                await _barService.UpdateBarAsync(existingBar);

                return Ok(existingBar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/bar")]
        public async Task<IActionResult> GetAllBars()
        {
            try
            {
                var bars = await _barService.GetAllBarsAsync();

                if (bars == null)
                {
                    return NotFound();
                }

                return Ok(bars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBarById(int id)
        {
            try
            {
                // Get the bar by ID using the _barService
                var bar = await _barService.GetBarByIdAsync(id);

                if (bar == null)
                {
                    // Return NotFound if the bar is not found
                    return NotFound();
                }

                // Return the bar with Ok status
                return Ok(bar);
            }
            catch (Exception ex)
            {
                // Return an error message with InternalServerError status
                return StatusCode(500, ex.Message);
            }
        }


    }
}
