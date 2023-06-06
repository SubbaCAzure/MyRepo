using Domain.Models;

namespace BBMS.Repository.Interfaces
{
    public interface IBarBeersRepository
    {
        //   Task CreateBarBeersAsync(BarBeers barBeers);

        Task<List<BarBeers>> GetSingleBarWithAllBeers();
        Task<List<BarBeers>> AddbarBeerAsync(BarBeers barBeer);


        Task<List<BarBeers>> GetAllBarsWithBeersAsync();


        Task InsertAsync(BarBeers barBeers);
    }
}
