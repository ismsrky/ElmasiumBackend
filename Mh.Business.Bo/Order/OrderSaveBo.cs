using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Order
{
    public class OrderSaveBo : BaseBo
    {
        public long BasketId { get; set; }

        public long DeliveryAddressId { get; set; }

        public string Notes { get; set; } // null. max length: 255
    }
}