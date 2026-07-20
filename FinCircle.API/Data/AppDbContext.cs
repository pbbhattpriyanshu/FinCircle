using Microsoft.EntityFrameworkCore;

namespace FinCircle.API.Data
{
    public class AppDbContext : DbContext
    {
        //Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
