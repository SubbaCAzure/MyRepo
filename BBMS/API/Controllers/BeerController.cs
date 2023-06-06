using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IBeerService _beerService;
        public BeerController(IBeerService beerService)
        {
            _beerService = beerService;
        }
         

        [HttpGet]
        public async Task<IActionResult> GetAllBeers()
        {
            try
            {
                var beers = await _beerService.GetAllBeersAsync();

                if (beers == null)
                {
                    return NotFound();
                }

                return Ok(beers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
         
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBeerById(int id)
        {
            try
            {                
                var beer = await _beerService.GetBeerByIdAsync(id);

                if (beer == null)
                {             
                    return NotFound();
                }
                
                return Ok(beer);
            }
            catch (Exception ex)
            {                
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/beer")]
        public async Task<IActionResult> PostBeer([FromBody] Beer beer)
        {
            try
            {
                // Validate the incoming beer object
                if (beer == null)
                {
                    return BadRequest("Invalid beer data");
                }

                // Add the beer to the repository or service
                await _beerService.CreateBeerAsync(beer);

                // Return a success response
                return Ok(beer);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate error response
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeer(int id, [FromBody] Beer updatedBeer)
        {
            try
            {
                // Get the existing beer by id
                var existingbeer = await _beerService.GetBeerByIdAsync(id);

                if (existingbeer == null)
                {
                    return NotFound();
                }

                // Update the properties of the existing beer with the values from updatedBeer
                existingbeer.Name = updatedBeer.Name;
                existingbeer.PercentageAlcoholByVolume = updatedBeer.PercentageAlcoholByVolume;
                // Update other properties as needed

                // Call the service or repository method to update the beer
                await _beerService.UpdateBeerAsync(existingbeer);

                return Ok(existingbeer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
