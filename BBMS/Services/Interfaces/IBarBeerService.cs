using Domain.Models;

namespace BBMS.Services.Interfaces
{
    public interface IBarBeerService
    {
        Task<List<BarBeers>> GetAllBarsWithBeersAsync();
        Task AddbarBeerAsync(BarBeers request);  
        Task<List<BarBeers>> GetSingleBarWithAllBeers();

        Task InsertAsync(BarBeers barBeers);
        

    }
}
