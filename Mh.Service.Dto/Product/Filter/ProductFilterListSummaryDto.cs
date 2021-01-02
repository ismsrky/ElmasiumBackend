using Mh.Service.Dto.Property;
using System.Collections.Generic;

namespace Mh.Service.Dto.Product.Filter
{
    public class ProductFilterListSummaryDto
    {
        public List<PropertyListDto> PropertyList { get; set; }

        public long Count { get; set; }
        public int PageSize { get; set; }
    }
}