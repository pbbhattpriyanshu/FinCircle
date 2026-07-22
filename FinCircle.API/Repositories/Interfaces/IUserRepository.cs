using FinCircle.API.Models;

namespace FinCircle.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmailAsync(string email);

        Task AddAsync(User user);

        Task<User?> GetByEmailAsync(string email);
    }
}