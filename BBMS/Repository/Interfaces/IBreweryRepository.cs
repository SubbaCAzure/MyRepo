using Domain.Models;

namespace BBMS.Repository.Interfaces
{
    public interface IBreweryRepository
    {
        Task<IEnumerable<Brewery>> GetAllBreweryAsync();
        Task<Brewery> GetBreweryByIdAsync(int id);
        Task CreateBreweryAsync(Brewery brewery);
        Task UpdateBreweryAsync(Brewery brewery);

    }
}
