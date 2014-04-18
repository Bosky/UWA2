using System.Collections.Generic;
using System.Linq;
using UWA.Core.BusinessLayer.Contracts;

namespace UWA.Core.DataAccessLayer
{
    public interface IMapLocationRepository
    {
        IList<MapLocation> GetLocations();
    }

    public class MapLocationRepository : IMapLocationRepository
    {
        public IList<MapLocation> GetLocations()
        {
            return DataLayer.UwaDatabase.GetItems<MapLocation>().ToList();
        }
    }
}
