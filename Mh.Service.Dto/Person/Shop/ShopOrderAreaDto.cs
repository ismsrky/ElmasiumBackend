using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Shop
{
    public class ShopOrderAreaDto
    {
        public long PersonId { get; set; }
        public long Id { get; set; }

        public Enums.AddressBoundaries AddressBoundaryId { get; set; }

        public int? AddressCountryId { get; set; }
        public int? AddressStateId { get; set; }
        public int? AddressCityId { get; set; }
        public int? AddressDistrictId { get; set; }

        public Enums.Currencies CurrencyId { get; set; }
        public Enums.OrderDeliveryTypes OrderDeliveryTypeId { get; set; }

        public List<ShopOrderAreaSubDto> SubList { get; set; }
    }
}