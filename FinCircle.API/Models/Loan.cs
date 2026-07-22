using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinCircle.API.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }

        [ForeignKey(nameof(MemberId))]
        public Member Member { get; set; } = null!;

        [Required]
        public decimal LoanAmount { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        [Required]
        public int DurationMonths { get; set; }

        [Required]
        [StringLength(200)]
        public string Purpose { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public DateTime? ApprovedDate { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}