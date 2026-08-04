using FinCircle.API.DTOs;

namespace FinCircle.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);

        Task<(string token, AuthResponseDto Response)> LoginAsync(LoginDto dto);
    }
}