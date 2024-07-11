using LocationApp.Business;
using LocationApp.Business.Constants;
using LocationApp.Business.Contracts;
using LocationApp.Entities;
using LocationApp.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocationApp.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CoordinateController : ControllerBase
    {
        private readonly ICoordinateService _coordinateService;

        public CoordinateController(ICoordinateService coordinateService)
        {
            _coordinateService = coordinateService;
        }
        [HttpGet]
        public Response GetAll()
        {
           return _coordinateService.GetAll();
        }

        [HttpGet("{id:int}")]
        public Response GetById([FromRoute] int id)
        {
            return _coordinateService.Get(id);
        }

        [HttpPost]
        public Response Add([FromBody] Coordinate coordinate)
        {
            return _coordinateService.Add(coordinate);            
        }

        [HttpPut("{id}")]
        public Response UpdateById([FromRoute] int id, [FromBody] Coordinate coordinate)
        {
           return _coordinateService.Update(id, coordinate);
        }

        [HttpDelete("{id}")]
        public Response DeleteById([FromRoute] int id)
        {
            return _coordinateService.Delete(id);
        }

    }
}
