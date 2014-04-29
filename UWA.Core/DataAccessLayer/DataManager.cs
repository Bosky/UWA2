using System.Collections.Generic;
using System.Linq;
using UWA.Core.BusinessLayer.Contracts;

namespace UWA.Core.DataAccessLayer
{
    public interface IDataManager
    {
        IList<MapLocation> GetLocations();
        IList<Person> GetPeople();
    }

    public class ORMRepository : IDataManager
    {
        public IList<MapLocation> GetLocations()
        {
            return DataLayer.UwaDatabase.GetItems<MapLocation>().ToList();
        }

        public IList<MapLocation> GetLocationByCriteria(string criteria)
        {
            return DataLayer.UwaDatabase.GetMapLocationsByCriteria(criteria);
        }

        public IList<Person> GetPeople()
        {
            return DataLayer.UwaDatabase.GetItems<Person>().ToList();
        }
    }
}
