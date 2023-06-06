using BBMS.Repository.Interfaces;
using BBMS.Services.Interfaces;
using Domain.Models;

namespace BBMS.Services
{
    public class BarService : IBarService
    {
        private readonly IBarRepository _barRepository;    
        public BarService(IBarRepository barRepository)
        {
            _barRepository = barRepository;           
        }
        public async Task<IEnumerable<Bar>> GetAllBarsAsync()
        {
            return await _barRepository.GetAllBarsAsync();
        }
        public async Task<Bar> GetBarByIdAsync(int id)
        {         
            var bar = await _barRepository.GetBarByIdAsync(id);
            return bar;
        }
        public async Task CreateBarAsync(Bar bar)
        {          
            await _barRepository.CreateBarAsync(bar);
        }
        public async Task UpdateBarAsync(Bar bar)
        {
            await _barRepository.UpdateBarAsync(bar);          
        }      
    }

}

