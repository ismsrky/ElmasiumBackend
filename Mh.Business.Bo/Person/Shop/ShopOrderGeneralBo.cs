using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Bo.Person.Shop
{
    public class ShopOrderGeneralBo : BaseBo
    {
        public long PersonId { get; set; }

        public bool TakesOrder { get; set; }
        public List<Enums.AccountTypes> OrderAccountList { get; set; }
        public string OrderAccountListRawJson { get; set; }

        public List<Enums.Currencies> OrderCurrencyList { get; set; }
        public string OrderCurrencyListRawJson { get; set; }
    }
}