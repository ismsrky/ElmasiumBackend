using System.Collections.Generic;

namespace Mh.Service.Dto.Property
{
    public class PropertyListDto
    {
        public int Id { get; set; } // Gruoup Id
        public string Name { get; set; } // Group name
        public string UrlName { get; set; } // Group url name

        public int Count { get; set; }

        public List<PropertyListDto> PropertyList { get; set; }
    }
}