using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinCircle.API.Models
{
    public class Contribution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }

        [ForeignKey(nameof(MemberId))]
        public Member Member { get; set; } = null!;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int ContributionMonth { get; set; }

        [Required]
        public int ContributionYear { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } = "Cash";

        public string Status { get; set; } = "Paid";

        public string Remarks { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}