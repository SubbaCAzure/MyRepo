using Domain.Models;

namespace Services.Interfaces
{
    public interface IBeerService
    {
       Task<IEnumerable<Beer>> GetAllBeersAsync();
       Task<Beer> GetBeerByIdAsync(int id);
        Task CreateBeerAsync(Beer beer);
        Task UpdateBeerAsync(Beer beer);
    }
}
