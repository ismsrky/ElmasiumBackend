using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Shop
{
    public class ShopOrderGeneralDto
    {
        public long PersonId { get; set; }

        public bool TakesOrder { get; set; }
        public List<Enums.AccountTypes> OrderAccountList { get; set; }
        public List<Enums.Currencies> OrderCurrencyList { get; set; }
    }
}