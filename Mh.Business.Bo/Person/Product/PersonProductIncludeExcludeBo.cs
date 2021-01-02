using Mh.Business.Bo.IncludeExclude;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Bo.Person.Product
{
    public class PersonProductIncludeExcludeBo : BaseBo
    {
        public long PersonProductId { get; set; }

        public bool IsInclude { get; set; }

        public List<IncludeExcludeBo> IncludeExcludeList { get; set; } // null. null means delete all include/exclude of that person product has and it also mean there is no include/exclude of that person product.
    }
}