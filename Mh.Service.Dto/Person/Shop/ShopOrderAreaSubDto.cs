namespace Mh.Service.Dto.Person.Shop
{
    public class ShopOrderAreaSubDto
    {
        public long Id { get; set; }

        public int AddressCountryId { get; set; }
        public int? AddressStateId { get; set; }
        public int? AddressCityId { get; set; }
        public int? AddressDistrictId { get; set; }
        public int? AddressLocalityId { get; set; }

        public decimal OrderMinPrice { get; set; }
        public Enums.OrderDeliveryTimes OrderDeliveryTimeId { get; set; }
    }
}