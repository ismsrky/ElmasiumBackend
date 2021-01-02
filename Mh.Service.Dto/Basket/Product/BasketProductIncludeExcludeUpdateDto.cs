using System.Collections.Generic;

namespace Mh.Service.Dto.Basket.Product
{
    public class BasketProductIncludeExcludeUpdateDto
    {
        public long BasketProductId { get; set; }

        public List<int> IncludeExcludeIdList { get; set; } // not null.
    }
}