using LocationApp.Entities;

namespace LocationApp.Business.Contracts
{
    public interface ICoordinateService
    {
        List<Coordinate> GetAll();

        Coordinate Get(int id);

        Coordinate Add(Coordinate coordinate);

        bool Delete(int id);

        Coordinate Update(int id, Coordinate coordinate);
    }
}

