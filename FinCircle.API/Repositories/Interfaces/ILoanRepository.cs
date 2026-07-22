using FinCircle.API.Models;

namespace FinCircle.API.Repositories.Interfaces
{
    public interface ILoanRepository
    {
        Task AddAsync(Loan loan);
        Task<List<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(int id);
        Task UpdateAsync(Loan loan);
        Task DeleteAsync(Loan loan);
        Task SaveChangesAsync();
    }
}