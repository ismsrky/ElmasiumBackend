﻿using System.Collections.Generic;

namespace Mh.Service.Dto.Basket.Product
{
    public class BasketProductSaveDto
    {
        public long DebtPersonId { get; set; }
        public long CreditPersonId { get; set; }

        public long ProductId { get; set; }
        public decimal Quantity { get; set; }

        public Enums.Currencies CurrencyId { get; set; }
        public bool IsFastSale { get; set; } // true means 'buy now' otherwise just add to the basket.

        public List<int> OptionIdList { get; set; } // not null if person product has option(s).
        public List<int> IncludeExcludeIdList { get; set; } // null
    }
}