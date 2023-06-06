using BBMS.Repository.Data;
using BBMS.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BBMS.Repository
{
    public class BreweryBeerRepository : IBreweryBeerRepository
    {
        private readonly DataContext _dbContext;

        public BreweryBeerRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Brewery> GetBreweryByIdAsync(int breweryId)
        {
            return await _dbContext.Breweries.FirstOrDefaultAsync(b => b.Id == breweryId);
        }
        public async Task<IEnumerable<Beer>> GetBeersByBreweryIdAsync(int breweryId)
        {
            return await _dbContext.Beers
                .Where(b => b.Id == breweryId)
                .ToListAsync();
        }
        public async Task<List<BreweryBeers>> GetAllBreweriesWithBeersAsync()
        { 
            return await Task.FromResult(new List<BreweryBeers>());
        }
        public async Task CreateBreweryBeersAsync(BreweryBeers breweryBeers)
        {

            // Add the BarBeers entity to the DbContext
            await _dbContext.BreweryBeers.AddAsync(breweryBeers);

            // Save changes to the data source
            await _dbContext.SaveChangesAsync();
        }

        Task<BreweryBeers> IBreweryBeerRepository.GetBreweryByIdAsync(int breweryId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<BreweryBeers>> IBreweryBeerRepository.GetBeersByBreweryIdAsync(int breweryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BreweryBeers>> GetSingleBreweryWithAllBeers()
        {
            throw new NotImplementedException();
        }
    }
}
