using Mh.Business.Bo.Product.Code;
using System;
using System.Collections.Generic;

namespace Mh.Business.Bo.Product.Filter
{
    public class ProductFilterPoolListBo
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }

        public Guid? PortraitImageUniqueId { get; set; }
        public Enums.FileTypes? PortraitImageFileTypeId { get; set; }

        public int? CategoryId { get; set; } // gets firstly from PersonProduct, if it is null gets from Product.

        public string ProductCodeListRawJson { get; set; } // not null
        public List<ProductCodeBo> ProductCodeList { get; set; } // not null

        public long? PersonProductId { get; set; }
        public decimal? Balance { get; set; }
    }
}