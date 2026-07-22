using System.ComponentModel.DataAnnotations;

namespace FinCircle.API.DTOs
{
    public class UpdateMemberDto
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
