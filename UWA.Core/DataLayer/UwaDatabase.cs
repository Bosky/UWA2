using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.IO;
using UWA.Core.BusinessLayer.Contracts;

namespace UWA.Core.DataLayer
{
    /// <summary>
    /// TaskDatabase builds on SQLite.Net and represents a specific database, in our case, the MWC DB.
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
        protected UwaDatabase(string path)
            : base(path)
        {
            // create the tables
            //CreateTable<Exhibitor>();
            //CreateTable<Session>();
            //CreateTable<Speaker>();
            //CreateTable<Favorite>();
            // FK
            //CreateTable<SessionSpeaker>();
            // these are really for caches
            //CreateTable<Tweet>();
            //CreateTable<RSSEntry>();
        }

        static UwaDatabase()
        {
            // set the db location
            DbLocation = DatabaseFilePath;

            // instantiate a new db
            Me = new UwaDatabase(DbLocation);
        }

        public static string DatabaseFilePath
        {
            get
            {
                return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "UwaDatabase.db3");
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

                // ---
                //return (from i in me.Table<T> ()
                //        where i.ID == id
                //        select i).FirstOrDefault ();

                // +++ To properly use Generic version and eliminate NotSupportedException
                // ("Cannot compile: " + expr.NodeType.ToString ()); in SQLite.cs
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

        // HACK: fixes UNHANDLED EXCEPTION: System.NotSupportedException: Cannot store type: MWC.BL.Favorite
        //public static int DeleteFavorite(int id)
        //{
        //    lock (locker)
        //    {
        //        return me.Delete(new Favorite() { ID = id });
        //    }
        //}

        //public static int DeleteItem<T>(int id) where T : BL.Contracts.IBusinessEntity, new()
        //{
        //    lock (locker)
        //    {
        //        return me.Delete<T>(new T() { ID = id });
        //    }
        //}

        //public static void ClearTable<T>() where T : BL.Contracts.IBusinessEntity, new()
        //{
        //    lock (locker)
        //    {
        //        me.Execute(string.Format("delete from \"{0}\"", typeof(T).Name));
        //    }
        //}

        //// helper for checking if database has been populated
        //public static int CountTable<T>() where T : BL.Contracts.IBusinessEntity, new()
        //{
        //    lock (locker)
        //    {
        //        string sql = string.Format("select count (*) from \"{0}\"", typeof(T).Name);
        //        var c = me.CreateCommand(sql, new object[0]);
        //        return c.ExecuteScalar<int>();
        //    }
        //}

        ////		public static IEnumerable<T> Query<T>(string query)
        ////		{
        ////			return _me.Query.
        ////		}

        //public static IEnumerable<Session> GetSessionsByStartDate(DateTime dateMin, DateTime dateMax)
        //{
        //    lock (locker)
        //    {
        //        return (from i in me.Table<Session>()
        //                where i.Start >= dateMin && i.Start <= dateMax
        //                select i).ToList();
        //    }
        //}


        ///*
        // * the following two queries are currently required because the Generic versions throw
        // * an exception on this line in SQLite.cs (Android ONLY)
        // * 1565:  throw new NotSupportedException ("Cannot compile: " + expr.NodeType.ToString ());
        // *
        // * ALSO now I've added some additional processing to the Session and Speaker to 'join' them
        // */
        ///// <summary>
        ///// Gets the Session AND linked Speakers
        ///// </summary>
        //public static Session GetSession(int id)
        //{
        //    lock (locker)
        //    {
        //        Session session = (from s in me.Table<Session>()
        //                           where s.ID == id
        //                           select s).FirstOrDefault();
        //        try
        //        { // bug occurs in simulator...???
        //            session.SpeakerKeys = (from ss in me.Table<SessionSpeaker>()
        //                                   where ss.SessionKey == session.Key
        //                                   select ss.SpeakerKey).ToList();
        //            var speakers = GetItems<Speaker>();

        //            var speakerInSession = (from sp in speakers
        //                                    where session.SpeakerKeys.Contains(sp.Key)
        //                                    select sp).ToList();

        //            session.Speakers = speakerInSession;
        //        }
        //        catch { }
        //        return session;
        //    }
        //}
        ///// <summary>
        ///// Gets the Session AND linked Speakers
        ///// </summary>
        //public static Session GetSessionWithKey(string key)
        //{
        //    lock (locker)
        //    {
        //        Session session = (from s in me.Table<Session>()
        //                           where s.Key == key
        //                           select s).FirstOrDefault();

        //        session.SpeakerKeys = (from ss in me.Table<SessionSpeaker>()
        //                               where ss.SessionKey == session.Key
        //                               select ss.SpeakerKey).ToList();
        //        var speakers = GetItems<Speaker>();

        //        var speakersInSession = (from sp in speakers
        //                                 where session.SpeakerKeys.Contains(sp.Key)
        //                                 select sp).ToList();

        //        session.Speakers = speakersInSession;

        //        return session;
        //    }
        //}
        ///// <summary>
        ///// Gets the Speaker AND linked Sessions
        ///// </summary>
        //public static Speaker GetSpeaker(int id)
        //{
        //    lock (locker)
        //    {
        //        Speaker speaker = (from s in me.Table<Speaker>()
        //                           where s.ID == id
        //                           select s).FirstOrDefault();

        //        var keys = (from ss in me.Table<SessionSpeaker>()
        //                    where ss.SpeakerKey == speaker.Key
        //                    select ss).ToList();
        //        // HACK: gets around "Default constructor not found for type System.String" error
        //        speaker.SessionKeys = keys.Select(x => x.SpeakerKey).ToList();

        //        var sessions = GetItems<Session>();

        //        var sessionsForSpeaker = (from se in sessions
        //                                  where speaker.SessionKeys.Contains(se.Key)
        //                                  select se).ToList();

        //        speaker.Sessions = sessionsForSpeaker;

        //        return speaker;
        //    }
        //}
        ///// <summary>
        ///// Gets the Speaker AND linked Sessions
        ///// </summary>
        //public static Speaker GetSpeakerWithKey(string key)
        //{
        //    lock (locker)
        //    {
        //        Speaker speaker = (from s in me.Table<Speaker>()
        //                           where s.Key == key
        //                           select s).FirstOrDefault();

        //        speaker.SessionKeys = (from ss in me.Table<SessionSpeaker>()
        //                               where ss.SpeakerKey == speaker.Key
        //                               select ss.SessionKey).ToList();
        //        var sessions = GetItems<Session>();

        //        var sessionsForSpeaker = (from se in sessions
        //                                  where speaker.SessionKeys.Contains(se.Key)
        //                                  select se).ToList();

        //        speaker.Sessions = sessionsForSpeaker;

        //        return speaker;
        //    }
        //}
        //public static Exhibitor GetExhibitor(int id)
        //{
        //    lock (locker)
        //    {
        //        //return DL.MwcDatabase.GetItem<Exhibitor> (id);
        //        return (from s in me.Table<Exhibitor>()
        //                where s.ID == id
        //                select s).FirstOrDefault();
        //    }
        //}
        //public static Exhibitor GetExhibitorWithName(string name)
        //{
        //    lock (locker)
        //    {
        //        return (from s in me.Table<Exhibitor>()
        //                where s.Name == name
        //                select s).FirstOrDefault();
        //    }
        //}
        //public static Tweet GetTweet(int id)
        //{
        //    lock (locker)
        //    {
        //        //return DL.MwcDatabase.GetItem<Tweet> (id);
        //        return (from s in me.Table<Tweet>()
        //                where s.ID == id
        //                select s).FirstOrDefault();
        //    }
        //}
        //public static RSSEntry GetNews(int id)
        //{
        //    lock (locker)
        //    {
        //        //return DL.MwcDatabase.GetItem<RSSEntry> (id);
        //        return (from s in me.Table<RSSEntry>()
        //                where s.ID == id
        //                select s).FirstOrDefault();
        //    }
        //}
    }
}