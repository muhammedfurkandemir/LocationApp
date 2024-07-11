using LocationApp.Entities;
using LocationApp.Utilities.Results;

namespace LocationApp.Business.Contracts
{
    public interface ICoordinateService
    {
        Response GetAll();

        Response Get(int id);

        Response Add(Coordinate coordinate);

        Response Delete(int id);

        Response Update(int id, Coordinate coordinate);
    }
}

