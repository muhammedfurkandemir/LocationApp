using LocationApp.Entities;
using LocationApp.Utilities.EntityFramework.Repositories;

namespace LocationApp.DataAccess.Abstract
{
    public interface IGeolocRepository : IRepository<Geoloc>
    {
    }
}
