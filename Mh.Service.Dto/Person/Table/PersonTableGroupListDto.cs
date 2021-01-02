using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Table
{
    public class PersonTableGroupListDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public Enums.PersonTableGroupStats PersonTableGroupStatId { get; set; }
        public int Order { get; set; }

        public List<PersonTableCountsDto> TableCountsList { get; set; } // not null. All types of stat come.
    }
}