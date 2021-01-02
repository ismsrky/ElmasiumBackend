using Mh.Business.Bo.Property;
using System.Collections.Generic;

namespace Mh.Business.Bo.Product.Filter
{
    public class ProductFilterListSummaryBo
    {
        public List<PropertyListBo> PropertyList { get; set; }
        public string PropertyListJson { get; set; }

        public long Count { get; set; }
        public int PageSize { get; set; }
    }
}