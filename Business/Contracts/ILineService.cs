using LocationApp.Dto;
using LocationApp.Entities;
using LocationApp.Utilities.Results;

namespace LocationApp.Business.Contracts
{
    public interface ILineService
    {
        Response GetAll();

        Response Get(int id);

        Response Add(SendGeolocDto _dto);

        Response Delete(int id);

        Response Update(int id, SendGeolocDto _dto);
    }
}
