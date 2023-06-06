using BBMS.Services;
using BBMS.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;


namespace BBMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryBeerController : ControllerBase
    {
        private readonly IBreweryBeerService _breweryBeerService;
        private readonly IBeerService _beerService;
        private readonly IBreweryService _breweryService;
        public BreweryBeerController(IBreweryBeerService breweryBeerService, IBeerService beerService, IBreweryService breweryService)
        {
            _breweryBeerService = breweryBeerService;
            _beerService = beerService;
            _breweryService = breweryService;
        }

        [HttpPost("/brewery/beer")]
        public async Task<IActionResult> PostBar([FromBody] BreweryBeers breweryBeers)
        {
            try
            {
                // Validate the incoming bar object
                if (breweryBeers == null)
                {
                    return BadRequest("Invalid bar data");
                }
                // Add the bar to the repository or service
                await _breweryBeerService.CreateBarBeersAsync(breweryBeers);

                // Return a success response
                return Ok(breweryBeers);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate error response
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/brewery/{breweryId}/beer")]
        public async Task<IActionResult> GetBreweryWithBeers(int breweryId)
        {
            try
            {
                try
                {
                    List<BreweryBeers> barsWithBeers = await _breweryBeerService.GetSingleBreweryWithAllBeers();
                    return Ok(barsWithBeers);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }

 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);  
            }
        }

        [HttpGet("/brewery/beer")]
        public async Task<IActionResult> GetAllBreweriesWithBeers()
        {
            var breweries = await _breweryBeerService.GetAllBreweriesWithBeersAsync();

            if (breweries == null)
            {
                return NotFound();
            }

            return Ok(breweries);
        }

    }
}
