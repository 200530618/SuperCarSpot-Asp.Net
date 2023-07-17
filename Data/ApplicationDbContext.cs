using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperCarSpot.Models;

namespace SuperCarSpot.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}