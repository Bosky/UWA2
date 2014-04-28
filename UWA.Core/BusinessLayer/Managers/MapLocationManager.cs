using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWA.Core.BusinessLayer.Contracts;
using UWA.Core.DataAccessLayer;

namespace UWA.Core.BusinessLayer.Managers
{
    public interface IMapLocationManager
    {
        IList<MapLocation> Search(string criteria);
    }

    public class MapLocationManager : IMapLocationManager
    {

        public IList<MapLocation> Search(string criteria)
        {
            var foundLocations = new List<MapLocation>();

            if (criteria == "foo")
                return foundLocations;
            else
            {
                var repo = new ORMRepository();
                return repo.GetLocationByCriteria(criteria);
            }
        }
    }
}
