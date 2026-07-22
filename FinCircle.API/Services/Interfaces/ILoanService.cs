using FinCircle.API.DTOs;
using FinCircle.API.Models;

namespace FinCircle.API.Services.Interfaces
{
    public interface ILoanService
    {
        Task CreateLoanAsync(CreateLoanDto dto);
        Task<List<Loan>> GetAllLoanAsync();
        Task<Loan?> GetLoanByIdAsync(int id);
        Task<bool> ApproveLoanAsync(int id);
        Task<bool> RejectLoanAsync(int id);
    }
}