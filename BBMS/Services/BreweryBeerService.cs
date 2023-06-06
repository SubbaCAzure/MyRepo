using BBMS.Repository;
using BBMS.Repository.Interfaces;
using BBMS.Services.Interfaces;
using Domain.Models;

namespace BBMS.Services
{
    public class BreweryBeerService : IBreweryBeerService
    {
        private readonly IBarRepository _barRepository;
        private readonly IBeerRepository _beerRepository;
        private readonly IBreweryBeerRepository _breweryBeerRepository;
        public BreweryBeerService(IBarRepository barRepository, IBeerRepository beerRepository, IBarBeersRepository barBeersRepository, IBreweryBeerRepository breweryBeerRepository)
        {
            _barRepository = barRepository;
            _beerRepository = beerRepository;
            _breweryBeerRepository = breweryBeerRepository;
        }

         
        public async Task CreateBarBeersAsync(BreweryBeers breweryBeers)
        {
            await _breweryBeerRepository.CreateBreweryBeersAsync(breweryBeers);
        }
        public async Task<BreweryBeers> GetBreweryByIdAsync(int breweryId)
        {
            return await _breweryBeerRepository.GetBreweryByIdAsync(breweryId);
        }

        public async Task<IEnumerable<BreweryBeers>> GetBeersByBreweryIdAsync(int breweryId)
        {
            return await _breweryBeerRepository.GetBeersByBreweryIdAsync(breweryId);
        }
        public async Task<List<BreweryBeers>> GetAllBreweriesWithBeersAsync()
        {
            return await _breweryBeerRepository.GetAllBreweriesWithBeersAsync();
        }

        
        public async Task<List<BreweryBeers>?> GetSingleBreweryWithAllBeers()
        {
            var bars = await _barRepository.GetAllBarsAsync();
            var beers = await _beerRepository.GetAllBeersAsync();
            var barBeers = await _breweryBeerRepository.GetSingleBreweryWithAllBeers();

            var barsWithBeers = bars.Select(bar =>
            {
                var brewryBeerIds = barBeers
                    .Where(bb => bb.BeerId == bar.Id)
                    .Select(bb => bb.Beers);

                return new BarBeers
                {
                    BarId = bar.Id,
                    BeerId = bar.Id,
                    Beers = beers.ToList(),
                };
            }).ToList();
            
            return barBeers;


        }

       
    }
}
