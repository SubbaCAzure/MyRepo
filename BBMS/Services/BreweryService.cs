using BBMS.Repository.Interfaces;
using BBMS.Services.Interfaces;
using Domain.Models;

namespace BBMS.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IBreweryRepository _breweryRepository;

        public BreweryService(IBreweryRepository breweryRepository)
        {
            _breweryRepository = breweryRepository;
        }
        public async Task CreateBreweryAsync(Brewery brewery)
        {
            await _breweryRepository.CreateBreweryAsync(brewery);
        }
        public async Task<IEnumerable<Brewery>> GetAllBrewerysAsync()
        {
            return await _breweryRepository.GetAllBreweryAsync();
        }
        public async Task<Brewery> GetBreweryByIdAsync(int id)
        {
            var brewery = await _breweryRepository.GetBreweryByIdAsync(id);
            return brewery;
        }
        public async Task UpdateBreweryAsync(Brewery brewery)
        {
            await _breweryRepository.UpdateBreweryAsync(brewery);
        } 
         
    }
}
