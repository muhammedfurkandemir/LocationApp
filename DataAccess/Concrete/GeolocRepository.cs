using LocationApp.DataAccess.Abstract;
using LocationApp.Entities;
using LocationApp.Utilities.EntityFramework;
using LocationApp.Utilities.EntityFramework.Repositories;

namespace LocationApp.DataAccess.Concrete
{
    public class GeolocRepository : RepositoryBase<Geoloc>, IGeolocRepository
    {
        public GeolocRepository(LocationContext locationContext) : base(locationContext)
        {
        }
    }
}
