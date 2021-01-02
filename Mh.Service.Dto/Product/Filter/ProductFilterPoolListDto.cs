using Mh.Service.Dto.Product.Code;
using System;
using System.Collections.Generic;

namespace Mh.Service.Dto.Product.Filter
{
    public class ProductFilterPoolListDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }

        public string PortraitImageUniqueIdStr { get; set; }

        public int? CategoryId { get; set; } // gets firstly from PersonProduct, if it is null gets from Product.

        public List<ProductCodeDto> ProductCodeList { get; set; }

        public long? PersonProductId { get; set; }
        public decimal? Balance { get; set; }
    }
}