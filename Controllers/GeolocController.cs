using LocationApp.Business.Contracts;
using LocationApp.Dto;
using LocationApp.Entities;
using LocationApp.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocationApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GeolocController : ControllerBase
    {
        private readonly ILineService _lineService;

        public GeolocController(ILineService lineService)
        {
            _lineService = lineService;
        }

        [HttpGet]
        public Response GetAll()
        {
            return _lineService.GetAll();
        }

        [HttpGet("{id:int}")]
        public Response GetById([FromRoute] int id)
        {
            return _lineService.Get(id);
        }

        [HttpPost]
        public Response Add(SendGeolocDto _dto)
        {
            return _lineService.Add(_dto);
        }

        [HttpPut("{id}")]
        public Response UpdateById(int id, SendGeolocDto _dto)
        {
            return _lineService.Update(id, _dto);
        }

        [HttpDelete("{id}")]
        public Response DeleteById([FromRoute] int id)
        {
            return _lineService.Delete(id);
        }
    }
}
