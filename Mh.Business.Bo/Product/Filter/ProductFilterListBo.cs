using System;
using System.Collections.Generic;

namespace Mh.Business.Bo.Product.Filter
{
    public class ProductFilterListBo
    {
        public long PersonProductId { get; set; } // not null
        public decimal OnlineSalePrice { get; set; } // not null

        public long StarCount { get; set; } // not null
        public long StarSum { get; set; } // not null

        public string Notes { get; set; } // null

        public long ProductId { get; set; } // not null
        public string ProductName { get; set; } // not null
        public Enums.ProductTypes ProductTypeId { get; set; } // not null

        public long ShopId { get; set; } // not null
        public string ShopFullName { get; set; } // not null
        public string ShopUrlName { get; set; } // not null
        public long ShopStarCount { get; set; } // not null
        public long ShopStarSum { get; set; } // not null
        public string ShopTypeName { get; set; } // not null
    }
}