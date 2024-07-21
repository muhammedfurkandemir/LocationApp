using LocationApp.DataAccess.Abstract;
using LocationApp.Utilities.EntityFramework;

namespace LocationApp.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LocationContext _locationContext;
        public ICoordinateRepository CoordinateRepository { get; }
        public IGeolocRepository GeolocRepository { get; }

        public UnitOfWork(LocationContext locationContext, ICoordinateRepository coordinateRepository,
            IGeolocRepository lineRepository)
        {
            _locationContext = locationContext;
            CoordinateRepository = coordinateRepository;
            GeolocRepository = lineRepository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Save()
        {
            return _locationContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _locationContext.Dispose();
            }
        }
    }
}
