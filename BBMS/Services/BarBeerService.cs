using BBMS.Repository;
using BBMS.Repository.Interfaces;
using BBMS.Services.Interfaces;
using Domain.Models;

namespace BBMS.Services
{
    public class BarBeerService : IBarBeerService
    {
        private readonly IBarRepository _barRepository;
        private readonly IBeerRepository _beerRepository;
        private readonly IBarBeersRepository _barBeersRepository;
        public BarBeerService(IBarRepository barRepository, IBeerRepository beerRepository, IBarBeersRepository barBeersRepository)
        {
            _barRepository = barRepository;
            _beerRepository = beerRepository;
            _barBeersRepository = barBeersRepository;
        }
        public async Task<List<BarBeers>> GetAllBarsWithBeersAsync()
        {
            var bars = await _barRepository.GetAllBarsAsync();
            var beers = await _beerRepository.GetAllBeersAsync();
            var barBeers = await _barBeersRepository.GetAllBarsWithBeersAsync();

            var barsWithBeers = bars.Select(bar =>
            {
                var barBeerIds = barBeers
                    .Where(bb => bb.BarId == bar.Id)
                    .Select(bb => bb.Beers);

                return new BarBeers
                {
                    BarId = bar.Id,
                    BeerId = bar.Id,
                    Beers = beers.ToList(),
                };
            }).ToList();

            return barsWithBeers;
        }

        public async Task AddbarBeerAsync(BarBeers request)
        {
            await _barBeersRepository.AddbarBeerAsync(request);
        }
        public async Task<List<BarBeers>> GetSingleBarWithAllBeers()
        {
            var bars = await _barRepository.GetAllBarsAsync();
            var beers = await _beerRepository.GetAllBeersAsync();
            var barBeers = await _barBeersRepository.GetAllBarsWithBeersAsync();

            var barsWithBeers = bars.Select(bar =>
            {
                var barBeerIds = barBeers
                    .Where(bb => bb.BarId == bar.Id)
                    .Select(bb => bb.Beers);
                return new BarBeers
                {
                    BarId = bar.Id,
                    BeerId = bar.Id,
                    Beers = beers.ToList(),
                };
            }).ToList();

            return barsWithBeers;
        }

        public async Task InsertAsync(BarBeers barBeers)
        {
            var bars = await _barRepository.GetAllBarsAsync();
            var beers = await _beerRepository.GetAllBeersAsync();
 


            await _barBeersRepository.InsertAsync(barBeers);
        }


    }
}
