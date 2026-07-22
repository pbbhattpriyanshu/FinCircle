using FinCircle.API.DTOs;
using FinCircle.API.Repositories.Interfaces;
using FinCircle.API.Services.Interfaces;

namespace FinCircle.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repository;

        public DashboardService(IDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            return await _repository.GetDashboardAsync();
        }
    }
}