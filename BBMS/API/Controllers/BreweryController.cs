using BBMS.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryController : ControllerBase
    {
        private readonly IBreweryService _breweryService;
        public BreweryController(IBreweryService breweryService)
        {
            _breweryService = breweryService;
        }
        [HttpGet("/brewery")]
        public async Task<IActionResult> GetAllBrewerys()
        {
            try
            {
                var brewery = await _breweryService.GetAllBrewerysAsync();

                if (brewery == null)
                {
                    return NotFound();
                }
                return Ok(brewery);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBreweryById(int id)
        {
            try
            {
                // Get the brewery by ID using the brewery
                var brewery = await _breweryService.GetBreweryByIdAsync(id);

                if (brewery == null)
                {                
                    return NotFound();
                }        
                return Ok(brewery);
            }
            catch (Exception ex)
            {           
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/brewery")]
        public async Task<IActionResult> PostBrewery([FromBody] Brewery brewery)
        {
            try
            {
                // Validate the incoming brewery object
                if (brewery == null)
                {
                    return BadRequest("Invalid brewery data");
                }
                // Add the brewery to the repository or service
                await _breweryService.CreateBreweryAsync(brewery);

                // Return a success response
                return Ok(brewery);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate error response
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrewery(int id, [FromBody] Brewery updatedBrewery)
        {
            try
            {             
                var existingBrewery = await _breweryService.GetBreweryByIdAsync(id);
                if (existingBrewery == null)
                {
                    return NotFound();
                }
                existingBrewery.Name = updatedBrewery.Name;

                await _breweryService.UpdateBreweryAsync(existingBrewery);

                return Ok(existingBrewery);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
