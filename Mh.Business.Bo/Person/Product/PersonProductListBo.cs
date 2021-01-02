using Mh.Business.Bo.Product.Code;
using Mh.Business.Bo.Product.Price;
using System;
using System.Collections.Generic;

namespace Mh.Business.Bo.Person.Product
{
    public  class PersonProductListBo
    {
        public long Id { get; set; } // Person product id

        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }

        public decimal VateRate { get; set; }

        public decimal Balance { get; set; }

        public string PriceRawJson { get; set; }
        public ProductPriceBo Price { get; set; }

        public Guid? PortraitImageUniqueId { get; set; }
        public Enums.FileTypes? PortraitImageFileTypeId { get; set; }

        public string CodeListRawJson { get; set; }
        public List<ProductCodeBo> CodeList { get; set; } // not null. At least one row that stock code.
    }
}