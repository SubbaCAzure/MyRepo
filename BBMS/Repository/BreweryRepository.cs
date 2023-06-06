using BBMS.Repository.Data;
using BBMS.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BBMS.Repository
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly DataContext _dbContext;

        public BreweryRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }        
        public async Task CreateBreweryAsync(Brewery brewery)
        {
            _dbContext.Breweries.Add(brewery);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Brewery>> GetAllBreweryAsync()
        {
            return await _dbContext.Breweries.ToListAsync();
        }
        public async Task<Brewery> GetBreweryByIdAsync(int id)
        {
            var brewery = await _dbContext.Breweries.FindAsync(id);
            return brewery;
        }
        public async Task UpdateBreweryAsync(Brewery brewery)
        {
            _dbContext.Breweries.Update(brewery);
            await _dbContext.SaveChangesAsync();
        }          

    }
}
