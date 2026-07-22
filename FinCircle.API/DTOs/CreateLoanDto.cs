using System.ComponentModel.DataAnnotations;

namespace FinCircle.API.DTOs
{
    public class CreateLoanDto
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public decimal LoanAmount { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        [Required]
        public int DurationMonths { get; set; }

        [Required]
        [StringLength(200)]
        public string Purpose { get; set; } = string.Empty;
    }
}