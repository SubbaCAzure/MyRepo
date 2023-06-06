using Domain.Models;

namespace BBMS.Repository.Interfaces
{
    public interface IBreweryBeerRepository
    {

        Task CreateBreweryBeersAsync(BreweryBeers breweryBeers);
        Task<BreweryBeers> GetBreweryByIdAsync(int breweryId);
        Task<IEnumerable<BreweryBeers>> GetBeersByBreweryIdAsync(int breweryId);
        Task<List<BreweryBeers>> GetAllBreweriesWithBeersAsync();
        Task<List<BreweryBeers>> GetSingleBreweryWithAllBeers();


    }
}
