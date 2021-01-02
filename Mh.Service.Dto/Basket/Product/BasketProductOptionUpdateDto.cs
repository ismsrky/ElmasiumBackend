using System.Collections.Generic;

namespace Mh.Service.Dto.Basket.Product
{
    public class BasketProductOptionUpdateDto
    {
        public long BasketProductId { get; set; }

        public List<int> OptionIdList { get; set; } // not null.
    }
}