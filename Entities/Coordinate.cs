using LocationApp.Entities.Abstract;

namespace LocationApp.Entities
{
    public class Coordinate:IEntity
    {
        public int id { get; set; }
        
        public string name { get; set; }

        public double coordinate_x { get; set; }

        public double coordinate_y { get; set; }
    }
}
