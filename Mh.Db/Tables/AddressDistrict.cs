using System;

namespace Mh.Db.Tables
{
    public class AddressDistrict
    {
        public int Id { get; set; } // int, not null
        public int CityId { get; set; } // int, not null
        public string Name { get; set; } // nvarchar(50), not null
    }
}
