using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Bo.Person.Shop
{
    public class ShopOrderAreaListBo
    {
        public int Id { get; set; }

        public Enums.AddressBoundaries AddressBoundaryId { get; set; }
        public string AddressName { get; set; }

        public Enums.Currencies CurrencyId { get; set; }
        public Enums.OrderDeliveryTypes OrderDeliveryTypeId { get; set; }

        public string SubListRawJson { get; set; }
        public List<ShopOrderAreaSubListBo> SubList { get; set; }
    }
}