using Domain.Models;

namespace BBMS.Services.Interfaces
{
    public interface IBreweryBeerService
    {
        Task CreateBarBeersAsync(BreweryBeers breweryBeers);
    
        Task<IEnumerable<BreweryBeers>> GetBeersByBreweryIdAsync(int breweryId);
        Task<List<BreweryBeers>> GetAllBreweriesWithBeersAsync();

  
        Task<List<BreweryBeers>> GetSingleBreweryWithAllBeers();


    }
}
 