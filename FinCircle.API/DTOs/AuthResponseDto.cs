namespace FinCircle.API.DTOs
{
    public class AuthResponseDto
    {
        public string Message { get; set; } = string.Empty;

        public string? Token { get; set; }

        public UserDto User { get; set; } = null!;
    }
}