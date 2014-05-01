
using System.Collections.Generic;
using UWA.Core.DataAccessLayer;

namespace UWA.Core.BusinessLayer.Managers
{
    public interface IMapLocationManager
    {
        IList<MapLocation> Search(string criteria);
        IList<MapLocationCategory> GetCategories();
    }

    /// <summary>
    /// The MapLocationManager handles all business level services related to MapLocations.
    /// Very limited use due to the small amount of business logic involved in the prototype but 
    /// leaving it out would be bad practise. 
    /// </summary>
    public class MapLocationManager : IMapLocationManager
    {
        private IDataManager _dataManager;

        public MapLocationManager() { _dataManager = new ORMRepository(); }

        public IList<MapLocationCategory> GetCategories()
        {
            return _dataManager.GetLocationCategories();
        }

        public IList<MapLocation> Search(string criteria)
        {
            var foundLocations = new List<MapLocation>();

            if (criteria == "foo")
                return foundLocations;
            else
            {
                return _dataManager.GetLocationsByCriteria(criteria);
            }
        }

    }
}
