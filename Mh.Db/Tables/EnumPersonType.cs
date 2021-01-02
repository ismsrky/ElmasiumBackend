using System;

namespace Mh.Db.Tables
{
    public class EnumPersonType
    {
        public int Id { get; set; } // int, not null
        public string Name { get; set; } // nvarchar(50), not null
        public int GroupId { get; set; } // int, not null
    }
}
