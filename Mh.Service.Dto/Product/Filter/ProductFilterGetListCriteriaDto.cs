using System.Collections.Generic;

namespace Mh.Service.Dto.Product.Filter
{
    public class ProductFilterGetListCriteriaDto
    {
        public string SearchWord { get; set; } // null. max length: 50
        public int ProductCategoryId { get; set; } // not null
        public List<string> PropertyList { get; set; } // null

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public int? PageNumber { get; set; } // null. null means 1.
    }
}