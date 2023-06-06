using BBMS.Repository.Data;
using BBMS.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BBMS.Repository
{
    public class BarRepository : IBarRepository
    {
        private readonly DataContext _dbContext;
        public BarRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Bar>> GetAllBarsAsync()
        {
          return await _dbContext.Bars.ToListAsync();
        }
        public async Task<Bar> GetBarByIdAsync(int id)
        {           
            var bar = await _dbContext.Bars.FindAsync(id);
            return bar;
        }
        public async Task CreateBarAsync(Bar bar)
        {           
            _dbContext.Bars.Add(bar);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateBarAsync(Bar bar)
        {
            _dbContext.Bars.Update(bar);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<BarBeers>> GetAllBarBeersAsync()
        {
            return await Task.FromResult(new List<BarBeers>());
        }


    }
}
