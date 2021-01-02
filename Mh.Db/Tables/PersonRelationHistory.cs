using System;

namespace Mh.Db.Tables
{
    public class PersonRelationHistory
    {
        public int Id { get; set; } // int, not null
        public int ParentPersonId { get; set; } // int, not null
        public int ChildPersonId { get; set; } // int, not null
        public int PersonRelationStateId { get; set; } // int, not null
        public DateTime CreateDateTime { get; set; } // datetime, not null
    }
}
