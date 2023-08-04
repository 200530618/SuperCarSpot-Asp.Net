using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperCarSpot.Models;

namespace SuperCarSpot.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Favourite> Favorites { get; set; } 
        public DbSet<FavouriteItem> FavouriteItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasOne(o => o.Favourite)
                .WithMany()
                .HasForeignKey(o => o.FavouriteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FavouriteItem>()
                .HasOne(o => o.Favourite)
                .WithMany(o => o.FavouriteItems)
                .HasForeignKey(o => o.FavouriteId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}