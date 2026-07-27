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
//DTO stands for Data Transfer Object. It is used to transfer data between the client and the server. It contains only the fields required for a specific request or response, helping improve security, reduce unnecessary data transfer, and decouple the API contract from the database model.