using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Basket.Product
{
    public class BasketProductQuantityUpdateBo : BaseBo
    {
        public long BasketProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}