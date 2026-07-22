using FinCircle.API.Models;

namespace FinCircle.API.Repositories.Interfaces
{
    public interface IContributionRepository
    {
        Task AddAsync(Contribution contribution);
        Task<List<Contribution>> GetAllAsync();
        Task<Contribution?> GetByIdAsync(int id);
        Task UpdateAsync(Contribution contribution);
        Task DeleteAsync(Contribution contribution);
    }
}