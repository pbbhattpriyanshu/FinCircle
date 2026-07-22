using FinCircle.API.Data;
using FinCircle.API.DTOs;
using FinCircle.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinCircle.API.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            return new DashboardDto
            {
                TotalMembers = await _context.Members.CountAsync(),

                ActiveMembers = await _context.Members.CountAsync(x => x.IsActive),

                TotalLoans = await _context.Loans.CountAsync(),

                ApprovedLoans = await _context.Loans.CountAsync(x => x.Status == "Approved"),

                PendingLoans = await _context.Loans.CountAsync(x => x.Status == "Pending"),
                TotalContributions = await _context.Contributions.CountAsync(),

                TotalContributionAmount = await _context.Contributions.SumAsync(x => x.Amount),
            };
        }
    }
}