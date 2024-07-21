using LocationApp.Business.Constants;
using LocationApp.Business.Contracts;
using LocationApp.DataAccess.UnitOfWork;
using LocationApp.Dto;
using LocationApp.Entities;
using LocationApp.Utilities.Results;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace LocationApp.Business
{
    public class GeolocManager : ILineService
    {
        public IUnitOfWork _unitOfWork;


        public GeolocManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Response Add(SendGeolocDto _dto)
        {
            try
            {
                WKTReader reader = new WKTReader();

                Geometry geom = reader.Read(_dto.Wkt);

                var addedLineEntity = new Geoloc
                {
                    name = _dto.Name,
                    geometry = geom
                };
                _unitOfWork.GeolocRepository.Add(addedLineEntity);
                _unitOfWork.Save();
                return new Response() { IsSuccess = true, Message = Messages.AdditionSuccess };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message };
            }
        }

        public Response Delete(int id)
        {
            try
            {
                var entity = _unitOfWork.GeolocRepository.Get(c => c.id == id);
                if (entity is null)
                {
                    return new Response() { Message = Messages.NotFound };
                }
                _unitOfWork.GeolocRepository.Delete(entity);
                _unitOfWork.Save();
                return new Response() { IsSuccess = true, Message = Messages.DeletionSuccess };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message };
            }
        }

        public Response Get(int id)
        {
            throw new NotImplementedException();
        }

        public Response GetAll()
        {
            try
            {
                var entities = _unitOfWork.GeolocRepository.GetAll();

                WKTWriter writer = new WKTWriter();

                var responseEntities=new List<ResponseGeolocDto>();

                foreach (var entity in entities)
                {
                    var wkt = writer.Write(entity.geometry);
                    responseEntities.Add(new ResponseGeolocDto
                    {
                        Id = entity.id,
                        Name = entity.name,
                        Wkt = wkt
                    });
                }               


                return new Response() { Data = responseEntities, IsSuccess = true, Message = Messages.IsSuccess };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message };
            }
        }

        public Response Update(int id, SendGeolocDto _dto)
        {
            try
            {
                var entity = _unitOfWork.GeolocRepository.Get(g => g.id == id);
                if (entity is null)
                {
                    return new Response() { Message = Messages.NotFound };
                }
                WKTReader reader = new WKTReader();

                Geometry geom = reader.Read(_dto.Wkt);

                
                entity.name = _dto.Name;
                entity.geometry = geom;

                _unitOfWork.GeolocRepository.Update(entity);

                _unitOfWork.Save();
                return new Response() { IsSuccess = true, Message = Messages.UpdateIsSuccess };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message };
            }
        }
    }
}
