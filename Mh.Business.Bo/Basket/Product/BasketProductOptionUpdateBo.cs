using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Bo.Basket.Product
{
    public class BasketProductOptionUpdateBo : BaseBo
    {
        public long BasketProductId { get; set; }

        public List<int> OptionIdList { get; set; } // not null.
    }
}