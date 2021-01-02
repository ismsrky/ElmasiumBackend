using Mh.Service.Dto.Product.Code;
using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Product
{
    public class PersonProductProfileDto
    {
        public long PersonProductId { get; set; } // not null
        public int CategoryId { get; set; } // not null
        public decimal? OnlineSalePrice { get; set; }
        public decimal SalePrice { get; set; }
        public long StarCount { get; set; }
        public long StarSum { get; set; }
        public decimal Balance { get; set; }
        public string Notes { get; set; }
        public bool IsSaleForOnline { get; set; }
        public bool IsTemporarilyUnavaible { get; set; }

        public bool IsShopOwner { get; set; }

        public long ProductId { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }
        public string ProductName { get; set; }

        public long ShopId { get; set; }
        public string ShopFullName { get; set; }
        public long ShopStarCount { get; set; }
        public long ShopStarSum { get; set; }
        public Enums.Currencies ShopDefaultCurrencyId { get; set; }
        public string ShopUrlName { get; set; }
        public bool ShopIsAvailable { get; set; }
        public string ShopTypeName { get; set; }

        public string PortraitImageUniqueIdStr { get; set; } // null

        public List<ProductCodeDto> CodeList { get; set; } // not null. At least one row that stock code.
    }
}