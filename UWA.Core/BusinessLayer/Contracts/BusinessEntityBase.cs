using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWA.Core.DataAccessLayer.SQLite;

namespace UWA.Core.BusinessLayer.Contracts
{
    public class BusinessEntityBase : IBusinessEntity
    {
        public BusinessEntityBase() {}
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
