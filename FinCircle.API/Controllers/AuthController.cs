using FinCircle.API.DTOs;
using FinCircle.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinCircle.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var response = await _authService.RegisterAsync(dto);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            Response.Cookies.Append("fincircleToken", result.token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,          // Use false only for local HTTP development
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(2),
                IsEssential = true
            });


            return Ok(result.Response);
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("fincircleToken");

            return Ok(new
            {
                message = "Logout successful."
            });
        }
    }
}