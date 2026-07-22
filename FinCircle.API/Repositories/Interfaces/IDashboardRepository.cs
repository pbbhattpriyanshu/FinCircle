using FinCircle.API.DTOs;

namespace FinCircle.API.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardDto> GetDashboardAsync();
    }
}