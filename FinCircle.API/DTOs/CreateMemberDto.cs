using System.ComponentModel.DataAnnotations;

namespace FinCircle.API.DTOs
{
    public class CreateMemberDto
    {
        [Required]
        [StringLength(50)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(80)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
    }
}
