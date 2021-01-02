using Mh.Business.Bo.Product.Code;
using Mh.Business.Bo.Product.Price;
using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;

namespace Mh.Business.Bo.Person.Product
{
    public class PersonProductBo : BaseBo
    {
        public long Id { get; set; }
        public decimal PurchaseVatRate { get; set; }
        public decimal SaleVatRate { get; set; }
        public int? CategoryId { get; set; }
        public decimal Balance { get; set; }

        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }

        public string CodeListRawJson { get; set; }
        public List<ProductCodeBo> CodeList { get; set; }

        public string PriceRawJson { get; set; }
        public ProductPriceBo Price { get; set; }

        public Guid? PortraitImageUniqueId { get; set; }
        public Enums.FileTypes? PortraitImageFileTypeId { get; set; }
    }
}