using BBMS.Repository.Data;
using BBMS.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BBMS.Repository
{
    public class BarBeersRepository : IBarBeersRepository
    {
        private readonly DataContext _dbContext;
        public BarBeersRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<BarBeers>> GetSingleBarWithAllBeers()
        {
            return await _dbContext.BarBeers.ToListAsync();
        } 
        public async Task AddbarBeerAsync(BarBeers barBeer)
        {
            await _dbContext.BarBeers.AddAsync(barBeer);
            await _dbContext.SaveChangesAsync();
        }     
        public async Task<List<BarBeers>> GetAllBarsWithBeersAsync()
        {
            return await _dbContext.BarBeers.ToListAsync();
        }
        Task<List<BarBeers>> IBarBeersRepository.AddbarBeerAsync(BarBeers barBeer)
        {
            throw new NotImplementedException();
        }


        public async Task InsertAsync(BarBeers barBeers)
        { 

            // Add the BarBeers entity to the DbContext
            await _dbContext.BarBeers.AddAsync(barBeers);

            // Save changes to the data source
            await _dbContext.SaveChangesAsync();
        }
    }
}
