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
    }
}
