
using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.IO;
using UWA.Core.BusinessLayer;

namespace UWA.Core.DataLayer
{
    /// <summary>
    /// UwaDatabase builds on SQLite.Net and represents a specific database, in our case, the UwaDatabase.db3
    /// It contains methods for retreival and persistance as well as db creation, all based on the 
    /// underlying ORM.
    /// </summary>
    public class UwaDatabase : SQLiteConnection
    {
        protected static UwaDatabase Me = null;
        protected static string DbLocation;

        static readonly object Locker = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="MWC.DL.MwcDatabase"/> MwcDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        protected UwaDatabase(string path): base(path) { }

        static UwaDatabase()
        {
            // set the db location
            DbLocation = DatabaseFilePath;

            // instantiate a new DB abstration
            Me = new UwaDatabase(DbLocation);
        }

        public static string DatabaseFilePath
        {
            get
            {
                var libraryPath = (string)null;
                #if __ANDROID__
                    libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                #endif
                var path = Path.Combine(libraryPath, "UwaDatabase.db3");
                return path;
            }
        }

        public static IEnumerable<T> GetItems<T>() where T : BusinessLayer.Contracts.IBusinessEntity, new()
        {
            lock (Locker)
            {
                return (from i in Me.Table<T>() select i).ToList();
            }
        }

        public static T GetItem<T>(int id) where T : BusinessLayer.Contracts.IBusinessEntity, new()
        {
            lock (Locker)
            {
                return Me.Table<T>().FirstOrDefault(x => x.ID == id);
            }
        }

        public static int SaveItem<T>(T item) where T : BusinessLayer.Contracts.IBusinessEntity
        {
            lock (Locker)
            {
                if (item.ID != 0)
                {
                    Me.Update(item);
                    return item.ID;
                }
                else
                {
                    return Me.Insert(item);
                }
            }
        }

        public static void SaveItems<T>(IEnumerable<T> items) where T : BusinessLayer.Contracts.IBusinessEntity
        {
            lock (Locker)
            {
                Me.BeginTransaction();

                foreach (T item in items)
                {
                    SaveItem<T>(item);
                }

                Me.Commit();
            }
        }

        /// <summary>
        /// Gets Maplocations by MapLocatioCategory
        /// </summary>
        public static IList<MapLocation> GetMapLocationsByCategory(string category)
        {
            lock (Locker)
            {
                List<MapLocation> mapLocations = (from m in Me.Table<MapLocation>()
                                                  where m.Category == category
                                                  select m).ToList();

                return mapLocations;
            }
        }

        /// <summary>
        /// Gets Maplocations by Criteria
        /// </summary>
        public static IList<MapLocation> GetMapLocationsByCriteria(string criteria)
        {
            lock (Locker)
            {
                List<MapLocation> mapLocations = (from m in Me.Table<MapLocation>()
                    where m.Name == criteria
                    select m).ToList();

                return mapLocations;
            }
        }
    }
}