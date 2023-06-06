using Domain.Models;

namespace BBMS.Repository.Interfaces
{
    public interface IBeerRepository
    {
        Task<IEnumerable<Beer>> GetAllBeersAsync();
        Task<Beer> GetBeerByIdAsync(int id); 
        Task CreateBeerAsync(Beer beer);
        Task UpdateBeerAsync(Beer beer);
    }
}
