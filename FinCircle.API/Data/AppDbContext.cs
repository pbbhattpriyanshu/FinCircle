using Microsoft.EntityFrameworkCore;
using FinCircle.API.Models;

namespace FinCircle.API.Data
{
    public class AppDbContext : DbContext
    {
        //Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Member> Members { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}
