using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.Database.Sqlite;

namespace UWA.Core.DataAccessLayer
{
    public class UwaDatabaseHelper : SQLiteOpenHelper
    {
        private const string DATABASE_NAME = "UwaDatabase.db3";
        private const int DATABASE_VERSION = 1;

        public UwaDatabaseHelper(Context context)
            : base(context, DATABASE_NAME, null, DATABASE_VERSION)
        {
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(@"
        CREATE TABLE MapLocation (
          ID INTEGER PRIMARY KEY AUTOINCREMENT,
          Longitude FLOAT NOT NULL,
          Latitude FLOAT NOT NULL
        )");
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXISTS MapLocation");

            OnCreate(db);
        }
    }
}
