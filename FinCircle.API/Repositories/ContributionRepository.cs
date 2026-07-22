using FinCircle.API.Data;
using FinCircle.API.Models;
using FinCircle.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinCircle.API.Repositories
{
    public class ContributionRepository : IContributionRepository
    {
        private readonly AppDbContext _context;

        //Contructor - injection
        public ContributionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Contribution contribution)
        {
            await _context.Contributions.AddAsync(contribution);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Contribution>> GetAllAsync()
        {
            return await _context.Contributions.ToListAsync();
        }

        public async Task<Contribution?> GetByIdAsync(int id)
        {
            return await _context.Contributions.FindAsync(id);
        }

        public async Task UpdateAsync(Contribution contribution)
        {
            _context.Contributions.Update(contribution);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Contribution contribution)
        {
            _context.Contributions.Remove(contribution);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
