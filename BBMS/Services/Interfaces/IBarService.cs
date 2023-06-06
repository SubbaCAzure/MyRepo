using Domain.Models;

namespace BBMS.Services.Interfaces
{
    public interface IBarService
    {
        Task<IEnumerable<Bar>> GetAllBarsAsync();        
        Task<Bar> GetBarByIdAsync(int id);
        Task CreateBarAsync(Bar bar);
        Task UpdateBarAsync(Bar bar);

        
    }
}
