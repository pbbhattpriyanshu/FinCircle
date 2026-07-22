using FinCircle.API.Data;
using FinCircle.API.Models;
using FinCircle.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinCircle.API.Repositories
{
    public class LoanRepository : ILoanRepository
    {
            private readonly AppDbContext _context;

            //Contructor - injection
            public LoanRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task AddAsync(Loan loan)
            {
                await _context.Loans.AddAsync(loan);
                await _context.SaveChangesAsync();
            }

            public async Task<List<Loan>> GetAllAsync()
            {
                return await _context.Loans.ToListAsync();
            }

            public async Task<Loan?> GetByIdAsync(int id)
            {
                return await _context.Loans.FindAsync(id);
            }

            public async Task UpdateAsync(Loan loan)
            {
                _context.Loans.Update(loan);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Loan loan)
            {
                _context.Loans.Remove(loan);
                await _context.SaveChangesAsync();
            }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
