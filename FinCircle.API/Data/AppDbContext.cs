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
    }
}
