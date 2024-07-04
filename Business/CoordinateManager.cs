using LocationApp.Business.Contracts;
using LocationApp.Entities;

namespace LocationApp.Business
{
    public class CoordinateManager
        //: ICoordinateService
    {
        public readonly static List<Coordinate> coordinateList = new List<Coordinate>();
        public Coordinate Add(Coordinate coordinate)
        {
            var point = new Coordinate();

            var random = new Random();

            point.Id = random.Next();
            point.X = coordinate.X;
            point.Y = coordinate.Y;
            point.Name = coordinate.Name;

            coordinateList.Add(point);
            
            return point;
        }

        public bool Delete(int id)
        {
            var deletedEntity = coordinateList.FirstOrDefault(x => x.Id == id);
            if (deletedEntity != null)
            {
                return false;
            }
            else
            {
                coordinateList.Remove(deletedEntity);
                return true;
            }

        }

        public Coordinate Get(int id)
        {
            var entity = coordinateList.FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                return null;
            }
            else
            {
                return entity;
            }
        }

        public List<Coordinate> GetAll()
        {
            return coordinateList;
        }

        public Coordinate Update(int id, Coordinate coordinate)
        {
            var updatedEntity = coordinateList.FirstOrDefault(x => x.Id == id);

            if (updatedEntity is null)
            {
                Console.WriteLine("null geldi");
                return null;
            }
            else
            {
                updatedEntity.Name = coordinate.Name;
                updatedEntity.X = coordinate.X;
                updatedEntity.Y = coordinate.Y;
                return updatedEntity;
            }
        }
    }
}
