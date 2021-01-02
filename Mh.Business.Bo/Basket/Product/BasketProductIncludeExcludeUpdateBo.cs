using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Bo.Basket.Product
{
    public class BasketProductIncludeExcludeUpdateBo : BaseBo
    {
        public long BasketProductId { get; set; }

        public List<int> IncludeExcludeIdList { get; set; } // not null.
    }
}