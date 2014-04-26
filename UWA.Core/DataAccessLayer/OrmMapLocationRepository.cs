using System.Collections.Generic;
using System.Linq;
using UWA.Core.BusinessLayer.Contracts;

namespace UWA.Core.DataAccessLayer
{
    public interface IMapLocationRepository
    {
        IList<MapLocation> GetLocations();
        IList<Person> GetPeople();
    }

    public class OrmMapLocationRepository : IMapLocationRepository
    {
        public IList<MapLocation> GetLocations()
        {
            return DataLayer.UwaDatabase.GetItems<MapLocation>().ToList();
        }

        public IList<Person> GetPeople()
        {
            return DataLayer.UwaDatabase.GetItems<Person>().ToList();
        }
    }
}
