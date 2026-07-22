using System.ComponentModel.DataAnnotations;

namespace FinCircle.API.DTOs
{
    public class CreateContributionDto
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int ContributionMonth { get; set; }

        [Required]
        public int ContributionYear { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } = "Cash";

        public string Remarks { get; set; } = string.Empty;
    }
}