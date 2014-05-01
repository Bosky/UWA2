
using System;
using SQLite;
using UWA.Core.BusinessLayer.Contracts;

namespace UWA.Core.BusinessLayer
{
    public class Person : IBusinessEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Office { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return String.Format(FirstName + " " + LastName);
        }
    }
    
    
}
