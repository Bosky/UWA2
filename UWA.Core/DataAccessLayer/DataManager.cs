
using System.Collections.Generic;
using System.Linq;
using UWA.Core.BusinessLayer;

namespace UWA.Core.DataAccessLayer
{
    public interface IDataManager
    {
        IList<MapLocation> GetLocations();
        IList<MapLocation> GetLocationsByCriteria(string criteria);
        IList<MapLocation> GetLocationsByCategory(string category); 
        IList<MapLocationCategory> GetLocationCategories();
        IList<Person> GetPeople();
    }

    public class ORMRepository : IDataManager
    {
        public IList<MapLocation> GetLocations()
        {
            return DataLayer.UwaDatabase.GetItems<MapLocation>().ToList();
        }

        public IList<MapLocation> GetLocationsByCriteria(string criteria)
        {
            // Fix issue in MapSearchActivity first.
            throw new System.NotImplementedException();
        }

        public IList<MapLocationCategory> GetLocationCategories()
        {
            return DataLayer.UwaDatabase.GetItems<MapLocationCategory>().ToList();
        }

        public IList<MapLocation> GetLocationsByCategory(string category)
        {
            return DataLayer.UwaDatabase.GetMapLocationsByCategory(category).ToList();
        }

        public IList<Person> GetPeople()
        {
            return DataLayer.UwaDatabase.GetItems<Person>().ToList();
        }


    }
}
