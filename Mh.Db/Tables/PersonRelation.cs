using System;

namespace Mh.Db.Tables
{
    public class PersonRelation
    {
        public int Id { get; set; } // int, not null
        public int ParentPersonId { get; set; } // int, not null
        public int ChildPersonId { get; set; } // int, not null
        public int PersonRelationId { get; set; } // int, not null
    }
}
