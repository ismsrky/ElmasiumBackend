using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Product
{
    public class PersonProductSeePriceGetCriteriaBo : BaseBo
    {
        // One of followings must be filled.
        public long? ProductId { get; set; }
        public string ProductCode { get; set; }

        public long ShopId { get; set; }

        public Enums.Currencies CurrencyId { get; set; }
    }
}