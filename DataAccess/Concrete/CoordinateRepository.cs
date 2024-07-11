using LocationApp.DataAccess.Abstract;
using LocationApp.Entities;
using LocationApp.Utilities.EntityFramework;
using LocationApp.Utilities.EntityFramework.Repositories;

namespace LocationApp.DataAccess.Concrete
{
    public class CoordinateRepository : RepositoryBase<Coordinate>, ICoordinateRepository
    {
        public CoordinateRepository(LocationContext locationContext) : base(locationContext)
        {
        }
    }
}
