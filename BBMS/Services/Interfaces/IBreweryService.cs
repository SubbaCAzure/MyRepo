using Domain.Models;

namespace BBMS.Services.Interfaces
{
    public interface IBreweryService
    {
        Task<IEnumerable<Brewery>> GetAllBrewerysAsync();
        Task<Brewery> GetBreweryByIdAsync(int id);
        Task CreateBreweryAsync(Brewery brewery);
        Task UpdateBreweryAsync(Brewery brewery);       

    }
}
