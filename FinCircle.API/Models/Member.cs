using System.ComponentModel.DataAnnotations;

namespace FinCircle.API.Models
{
    public class Member
    {
        [Key] //Primary key
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string MemberCode { get; set; } = string.Empty;

        [Required] //value = not null
        [StringLength(50)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(80)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        public DateTime JoinedOn { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

    }
}
