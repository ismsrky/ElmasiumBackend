using System;

namespace Mh.Db.Tables
{
    public class AddressLocality
    {
        public int Id { get; set; } // int, not null
        public int DistrictId { get; set; } // int, not null
        public string Name { get; set; } // nvarchar(250), not null
    }
}
