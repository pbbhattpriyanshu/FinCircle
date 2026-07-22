using FinCircle.API.DTOs;
using FinCircle.API.Models;

namespace FinCircle.API.Services.Interfaces
{
    public interface IContributionService
    {
        Task CreateContributionAsync(CreateContributionDto dto);

        Task<List<Contribution>> GetAllContributionAsync();

        Task<Contribution?> GetContributionByIdAsync(int id);
    }
}