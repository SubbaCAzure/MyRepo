using BBMS.Repository.Data;
using BBMS.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace BBMS.Repository
{
    public class BeerRepository : IBeerRepository
    {
        private readonly DataContext _dbContext;
        public BeerRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Beer>> GetAllBeersAsync()
        {
            return await _dbContext.Beers.ToListAsync();
        }
        public async Task<Beer> GetBeerByIdAsync(int id)
        {
            return await _dbContext.Beers.FindAsync(id);
        }
        public async Task CreateBeerAsync(Beer beer)
        {
            _dbContext.Beers.Add(beer);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateBeerAsync(Beer beer)
        { 
            _dbContext.Beers.Update(beer);
            await _dbContext.SaveChangesAsync();
        }




    }
}
