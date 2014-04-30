﻿using SQLite;

namespace UWA.Core.BusinessLayer.Contracts
{
    public class MapLocation : IBusinessEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }     
    }
}
