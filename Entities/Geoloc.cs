using LocationApp.Entities.Abstract;
using NetTopologySuite.Geometries;

namespace LocationApp.Entities
{
    public class Geoloc : IEntity
    {
        public int id { get; set; }

        public string name { get; set; }

        public Geometry geometry { get; set; }
    }
}
