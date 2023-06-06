using BBMS.Repository.Interfaces;
using Domain.Models;
using Services.Interfaces;

namespace Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepository;
        public BeerService(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }        
        public async Task<IEnumerable<Beer>> GetAllBeersAsync()
        {
            var beers = await _beerRepository.GetAllBeersAsync();
            return beers;
        }
        public Task<Beer> GetBeerByIdAsync(int id)
        {
            var beers = _beerRepository.GetBeerByIdAsync(id);
            return beers;
        }       
        public async Task CreateBeerAsync(Beer beer)
        {
            await _beerRepository.CreateBeerAsync(beer);
        }
        public async Task UpdateBeerAsync(Beer beer)
        {
            await _beerRepository.UpdateBeerAsync(beer);
        }
    }
}
