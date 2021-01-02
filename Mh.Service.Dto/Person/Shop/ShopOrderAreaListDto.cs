using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Shop
{
    public class ShopOrderAreaListDto
    {
        public int Id { get; set; }

        public Enums.AddressBoundaries AddressBoundaryId { get; set; }
        public string AddressName { get; set; }

        public Enums.Currencies CurrencyId { get; set; }
        public Enums.OrderDeliveryTypes OrderDeliveryTypeId { get; set; }

        public List<ShopOrderAreaSubListDto> SubList { get; set; }
    }
}