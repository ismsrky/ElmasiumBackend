using System;

namespace Mh.Db.Tables
{
    public class PersonAccount
    {
        public int Id { get; set; } // int, not null
        public int PersonId { get; set; } // int, not null
        public string Name { get; set; } // nvarchar(50), not null
        public int AccountTypeId { get; set; } // int, not null
        public int StatId { get; set; } // int, not null
        public decimal Balance { get; set; } // decimal(18,2), not null
        public string Notes { get; set; } // nvarchar(250), null
        public int CreatePersonId { get; set; } // int, not null
        public DateTime CreateDate { get; set; } // datetime, not null
        public int? UpdatePersonId { get; set; } // int, null
        public DateTime? UpdateDate { get; set; } // datetime, null
    }
}
