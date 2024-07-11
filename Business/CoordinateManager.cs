using LocationApp.Business.Constants;
using LocationApp.Business.Contracts;
using LocationApp.DataAccess.Abstract;
using LocationApp.DataAccess.UnitOfWork;
using LocationApp.Entities;
using LocationApp.Utilities.Results;

namespace LocationApp.Business
{
    public class CoordinateManager : ICoordinateService
    {
        public IUnitOfWork _unitOfWork;


        public CoordinateManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response Add(Coordinate coordinate)
        {
            try
            {
                _unitOfWork.CoordinateRepository.Add(coordinate);
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
                var entity = _unitOfWork.CoordinateRepository.Get(c => c.id == id);
                if (entity is null)
                {
                    return new Response() { Message = Messages.NotFound };
                }
                _unitOfWork.CoordinateRepository.Delete(entity);
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
            try
            {
                var entity = _unitOfWork.CoordinateRepository.Get(c => c.id == id);
                if(entity is null)
                {
                    return new Response() { Message = Messages.NotFound };
                }
                return new Response() { Data=entity, IsSuccess = true, Message = Messages.DeletionSuccess };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message };
            }
        }

        public Response GetAll()
        {
            try
            {
                var entities = _unitOfWork.CoordinateRepository.GetAll();
                return new Response() { Data = entities, IsSuccess = true, Message = Messages.IsSuccess };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message };
            }
        }

        public Response Update(int id, Coordinate coordinate)
        {
            try
            {
                var entity = _unitOfWork.CoordinateRepository.Get(c => c.id == id);
                if (entity is null)
                {
                    return new Response() { Message = Messages.NotFound };
                }
                _unitOfWork.CoordinateRepository.Update(coordinate);
                return new Response() { IsSuccess = true, Message = Messages.UpdateIsSuccess };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message };
            }
        }
    }
}
