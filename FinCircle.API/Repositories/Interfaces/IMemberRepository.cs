using FinCircle.API.Models;

namespace FinCircle.API.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        Task AddAsync(Member member);
        Task<Member?> GetByIdAsync(int id);
        Task<List<Member>> GetAllAsync();
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByPhoneAsync(string phoneNumber);
        Task UpdateAsync(Member member);
        Task DeleteAsync(Member member);
    }
}
