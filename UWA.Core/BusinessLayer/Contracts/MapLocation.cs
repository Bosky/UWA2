
namespace UWA.Core.BusinessLayer.Contracts
{
    public class MapLocation : IBusinessEntity
    {
        public int ID { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        
    }
}
