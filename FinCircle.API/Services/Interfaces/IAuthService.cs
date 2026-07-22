using FinCircle.API.DTOs;

namespace FinCircle.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}