using LocationApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocationApp.Utilities.EntityFramework
{
    public class LocationContext:DbContext
    {
        public LocationContext(DbContextOptions<LocationContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Coordinate> coordinates { get; set; }

        public DbSet<Geoloc> lines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Geoloc>()
                .Property(l => l.geometry)
                .HasColumnType("geometry");
        }
    }
}
