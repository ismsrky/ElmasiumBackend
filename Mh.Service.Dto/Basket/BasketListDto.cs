using System.Collections.Generic;

namespace Mh.Service.Dto.Basket
{
    public class BasketListDto
    {
        public long Id { get; set; }
        public long DebtPersonId { get; set; }
        public Enums.Currencies CurrencyId { get; set; }
        public decimal GrandTotal { get; set; }

        public long ShopId { get; set; }
        public string ShopFullName { get; set; } // not null
        public string ShopUrlName { get; set; } // not null
        public long ShopStarCount { get; set; }
        public long ShopStarSum { get; set; }

        public double CreateDateNumber { get; set; }
        public double? UpdateDateNumber { get; set; }

        public List<Product.BasketProductListDto> ProductList { get; set; }
    }
}