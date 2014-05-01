
using SQLite;
using UWA.Core.BusinessLayer.Contracts;

namespace UWA.Core.BusinessLayer
{
    /// <summary>
    /// MapLocation is the business model for a location presentable on the Maps feature.
    /// At this moment, this entity violates the interface segregation priciple as the interfaces exposed both 
    /// geographical location and information on a place itself (name and category). 
    /// TODO: Consider new interfaces
    /// </summary>
    public class MapLocation : IBusinessEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }     
    }
}
