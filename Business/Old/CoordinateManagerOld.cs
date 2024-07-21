using LocationApp.Business.Contracts;
using LocationApp.Entities;

namespace LocationApp.Business.Old
{
    public class CoordinateManagerOld
    //: ICoordinateService
    {
        public readonly static List<Coordinate> coordinateList = new List<Coordinate>();
        public Coordinate Add(Coordinate coordinate)
        {
            var point = new Coordinate();

            var random = new Random();

            point.id = random.Next();
            point.coordinate_x = coordinate.coordinate_x;
            point.coordinate_y = coordinate.coordinate_y;
            point.name = coordinate.name;

            coordinateList.Add(point);

            return point;
        }

        public bool Delete(int id)
        {
            var deletedEntity = coordinateList.FirstOrDefault(x => x.id == id);
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
            var entity = coordinateList.FirstOrDefault(x => x.id == id);
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
            var updatedEntity = coordinateList.FirstOrDefault(x => x.id == id);

            if (updatedEntity is null)
            {
                Console.WriteLine("null geldi");
                return null;
            }
            else
            {
                updatedEntity.name = coordinate.name;
                updatedEntity.coordinate_x = coordinate.coordinate_x;
                updatedEntity.coordinate_y = coordinate.coordinate_y;
                return updatedEntity;
            }
        }
    }
}
