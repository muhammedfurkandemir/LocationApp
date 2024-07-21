using LocationApp.DataAccess.Abstract;

namespace LocationApp.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICoordinateRepository CoordinateRepository { get; }
        IGeolocRepository GeolocRepository { get; }
        int Save();
    }
}
