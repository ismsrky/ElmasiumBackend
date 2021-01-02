namespace Mh.Service.Dto.Person.Shop
{
    public class ShopOrderAreaSubListDto
    {
        public int Id { get; set; }

        public string AddressName { get; set; }

        public decimal OrderMinPrice { get; set; }
        public Enums.OrderDeliveryTimes OrderDeliveryTimeId { get; set; }

        public bool HasStates { get; set; }
    }
}