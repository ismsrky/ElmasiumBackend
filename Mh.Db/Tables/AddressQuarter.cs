using System;

namespace Mh.Db.Tables
{
    public class AddressQuarter
    {
        public int Id { get; set; } // int, not null
        public int LocalityId { get; set; } // int, not null
        public string Name { get; set; } // nvarchar(50), not null
    }
}
