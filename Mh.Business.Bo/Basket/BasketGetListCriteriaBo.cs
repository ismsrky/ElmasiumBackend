using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Basket
{
    public class BasketGetListCriteriaBo : BaseBo
    {
        public long DebtPersonId { get; set; }
        public Enums.Currencies CurrencyId { get; set; }

        public long? BasketId { get; set; }
    }
}