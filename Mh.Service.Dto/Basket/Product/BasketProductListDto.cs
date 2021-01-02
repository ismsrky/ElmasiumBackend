using Mh.Service.Dto.IncludeExclude;
using Mh.Service.Dto.Option;
using Mh.Service.Dto.Product.Code;
using System.Collections.Generic;

namespace Mh.Service.Dto.Basket.Product
{
    public class BasketProductListDto
    { 
        // Basket product
        public long Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal GrandTotal { get; set; }
        public string Notes { get; set; }

        // Product
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }

        // Person product
        public long PersonProductId { get; set; }
        public int CategoryId { get; set; }
        public decimal Balance { get; set; }
        public long StarCount { get; set; }
        public long StarSum { get; set; }

        public List<ProductCodeDto> CodeList { get; set; } // not null and at least one row that stock code.

        public string PortraitImageUniqueIdStr { get; set; } // null

        public List<OptionDto> OptionList { get; set; } // null
        public List<IncludeExcludeDto> IncludeExcludeList { get; set; } // null
    }
}