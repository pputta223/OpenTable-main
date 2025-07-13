using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenTable.Models.DataLayer.Configuration;
using OpenTable.Models.DomainModels;

namespace OpenTable.Models.DataLayer
{
    public class OpenTableContext : IdentityDbContext<User>
    {
        public OpenTableContext(DbContextOptions<OpenTableContext> options)
            : base(options)
        {
        }

        public DbSet<Metropolis> Metropolises { get; set; } = null!;
        public DbSet<Restaurant> Restaurants { get; set; } = null!;
        public DbSet<AppUser> AppUsers { get; set; } = null!;
        public DbSet<Price> Prices { get; set; } = null!;
        public DbSet<Cuisine> Cuisines { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>()
                .HasKey(u => u.Id);

            modelBuilder.ApplyConfiguration(new ConfigureCuisine());
            modelBuilder.ApplyConfiguration(new ConfigureMetropolis());
            modelBuilder.ApplyConfiguration(new ConfigurePrice());
            modelBuilder.ApplyConfiguration(new ConfigureRestaurant());
            modelBuilder.Entity<Reservation>()
                .HasIndex(r => new { r.RestaurantId, r.Date, r.TimeSlot });
        }
    }
}
