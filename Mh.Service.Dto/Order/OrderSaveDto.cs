namespace Mh.Service.Dto.Order
{
    public class OrderSaveDto
    {
        public long BasketId { get; set; }

        public long DeliveryAddressId { get; set; }

        public string Notes { get; set; } // null. max length: 255
    }
}