using Domain.Models;

namespace BBMS.Repository.Interfaces
{
    public interface IBarRepository
    {
        Task<IEnumerable<Bar>> GetAllBarsAsync();
        Task<Bar> GetBarByIdAsync(int id);
        Task CreateBarAsync(Bar bar);
        Task UpdateBarAsync(Bar bar);


        
    }
}
