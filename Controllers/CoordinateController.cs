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
        public Response Get()
        {
            var _response = new Response();
            try
            {
                var coordinateList = _coordinateService.GetAll();
                if (coordinateList.Count < 1)
                {
                    _response.Message = Messages.NoValueOfObject;
                    return _response;
                }
                _response.Data = coordinateList;
                _response.IsSuccess = true;
                return _response;
            }
            catch (Exception ex)
            {
                _response.Message = Messages.UnexpectedError;
                return _response;
            }
        }

        [HttpGet("{id:int}")]
        public Response GetById([FromRoute] int id)
        {
            var _response = new Response();
            try
            {
                var coordinate = _coordinateService.Get(id);
                if (coordinate == null)
                {
                    _response.Message = Messages.NotFoundId;
                    return _response;
                }
                _response.Data = coordinate;
                _response.IsSuccess = true;
                return _response;
            }
            catch (Exception ex)
            {
                _response.Message = Messages.UnexpectedError;
                return _response;
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] Coordinate coordinate)
        {
            var entity = _coordinateService.Add(coordinate);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new Response { Data = entity, IsSuccess = true });
        }

        [HttpPut("{id}")]
        public Response UpdateById([FromRoute] int id, [FromBody] Coordinate coordinate)
        {
            var _response = new Response();
            try
            {
                var entity = _coordinateService.Update(id, coordinate);
                if (entity == null)
                {
                    _response.Message = Messages.NotFoundId;
                    return _response;
                }
                _response.Data = entity;
                    _response.IsSuccess = true;
                return _response;
            }
            catch (Exception ex)
            {
                _response.Message = Messages.UnexpectedError;
                return _response;
            }
        }

        [HttpDelete("{id}")]
        public Response DeleteById([FromRoute] int id)
        {
            var _response = new Response();
            try
            {
                var entityIsDeleted = _coordinateService.Delete(id);
                if (!entityIsDeleted)
                {
                    _response.Message = Messages.NotFoundId;
                    return _response;
                }
                _response.Data = entityIsDeleted;
                _response.IsSuccess = true;
                return _response;
            }
            catch (Exception)
            {
                _response.Message = Messages.UnexpectedError;
                return _response;
            }
        }

    }
}
