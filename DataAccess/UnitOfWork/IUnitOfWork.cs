using LocationApp.DataAccess.Abstract;

namespace LocationApp.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICoordinateRepository CoordinateRepository { get; }
        int Save();
    }
}
