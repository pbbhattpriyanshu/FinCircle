using FinCircle.API.DTOs;

namespace FinCircle.API.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardAsync();
    }
}