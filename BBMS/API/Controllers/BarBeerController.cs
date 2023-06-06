using BBMS.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BBMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarBeerController : ControllerBase
    {
        private readonly IBarBeerService _barBeerService;
        private readonly IBarService _barService;
        private readonly IBeerService _beerService;
        public BarBeerController(IBarBeerService barBeerService, IBarService barService, IBeerService beerService)
        {
            _barBeerService = barBeerService;
            _barService = barService;
            _beerService = beerService;
        }


        [HttpPost("/bar/beer")]
        public async Task<IActionResult> InsertBarBeerLink([FromBody] BarBeers request)
        {
            try
            {
                // Validate the request payload
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Bar barIds = await _barService.GetBarByIdAsync(request.BarId);
                Beer beers = await _beerService.GetBeerByIdAsync(request.BeerId);

                // Insert the bar beer link using the repository
                await _barBeerService.InsertAsync(request);

                // Return a success response
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        //[HttpPost("/bar/beer")]
        //public async Task<IActionResult> InsertBarBeerLink([FromBody] BarBeers request)
        //{
        //    try
        //    {
        //        // Check if the bar exists
        //        Bar bar = await _barService.GetBarByIdAsync(request.BarId);
        //        if (bar == null)
        //        {
        //            return NotFound("Bar not found");
        //        }
        //        // Check if the beer exists
        //        Beer beer = await _beerService.GetBeerByIdAsync(request.BeerId);
        //        if (beer == null)
        //        {
        //            return NotFound("Beer not found");
        //        }
        //        // Create the bar beer link
        //        BarBeers barBeerLink = new BarBeers
        //        {
        //            BarId = request.BarId,
        //            BeerId = request.BeerId
        //        };
        //        // Add the bar beer link to the repository
        //        await _barBeerService.AddbarBeerAsync(request);
        //        return Ok("Bar beer link inserted successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

        [HttpGet("/bar/{barId}/beer")]
        public async Task<IActionResult> GetSingleBarWithAllBeers(int barId)
        {
            try
            {
                List<BarBeers> barsWithBeers = await _barBeerService.GetSingleBarWithAllBeers();
                return Ok(barsWithBeers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("/bar/beer")]
        public async Task<IActionResult> GetAllBarsWithBeersAsync()
        {
            List<BarBeers> barsWithBeers = await _barBeerService.GetAllBarsWithBeersAsync();
            if (barsWithBeers == null || barsWithBeers.Count == 0)
            {
                return NotFound();
            }

            return Ok(barsWithBeers);
        }

    }
}
