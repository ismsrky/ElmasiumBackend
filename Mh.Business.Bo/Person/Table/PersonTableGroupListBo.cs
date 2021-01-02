using System.Collections.Generic;

namespace Mh.Business.Bo.Person.Table
{
    public class PersonTableGroupListBo
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public Enums.PersonTableGroupStats PersonTableGroupStatId { get; set; }

        public int Order { get; set; }

        public string TableCountsRawJson { get; set; }
        public List<PersonTableCountsBo> TableCountsList { get; set; } // not null. All types of stat come.
    }
}