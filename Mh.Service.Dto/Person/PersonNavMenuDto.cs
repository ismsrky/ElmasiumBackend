using System.Collections.Generic;

namespace Mh.Service.Dto.Person
{
    public class PersonNavMenuDto
    {
        public int Id { get; set; } // not null
        public string Name { get; set; } // not null
        public string Url { get; set; } // null
        public string IconClass { get; set; } // null
        public string IconColor { get; set; } // null

        public int Range { get; set; } // not null

        public Enums.NavMenuPositions Position { get; set; }

        public List<PersonNavMenuDto> SubMenuList { get; set; } // null
    }
}