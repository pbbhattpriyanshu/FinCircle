using Microsoft.EntityFrameworkCore;
using FinCircle.API.Models;

namespace FinCircle.API.Data
{
    //Inherit DbContext - Entity Frame Work
    public class AppDbContext : DbContext
    {
        //Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //Mapping Services to Db Tables
        public DbSet<Member> Members { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}
//AppDbContext - Bridge between Application and Database.