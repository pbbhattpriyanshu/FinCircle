namespace FinCircle.API.DTOs
{
    public class DashboardDto
    {
        public int TotalMembers { get; set; }
        public int ActiveMembers { get; set; }
        public int TotalLoans { get; set; }
        public int ApprovedLoans { get; set; }
        public int PendingLoans { get; set; }
    }
}