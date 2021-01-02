using Mh.Service.Dto.IncludeExclude;
using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Product
{
    public class PersonProductIncludeExcludeDto
    {
        public long PersonProductId { get; set; }

        public bool IsInclude { get; set; }

        public List<IncludeExcludeDto> IncludeExcludeList { get; set; } // null. null means delete all include/exclude of that person product has and it also mean there is no include/exclude of that person product.
    }
}