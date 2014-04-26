using SQLite;

namespace UWA.Core.BusinessLayer.Contracts
{
    public class Person : IBusinessEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Office { get; set; }
        public string Address { get; set; }
    }
}
