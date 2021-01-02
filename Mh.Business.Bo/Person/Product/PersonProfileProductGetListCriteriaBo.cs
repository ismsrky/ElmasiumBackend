using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Product
{
    public class PersonProfileProductGetListCriteriaBo : BaseBo
    {
        public long? PersonProductId { get; set; } // null. If any value present, other params will be ignored.

        public long ShopId { get; set; }
        public Enums.Currencies CurrencyId { get; set; }

        public int CategoryId { get; set; }
        public Enums.StockStats? StockStatId { get; set; }

        public bool? IsSaleForOnline { get; set; }
        public bool? IsTemporarilyUnavaible { get; set; }

        public string ProductNameCode { get; set; } // null. max lenght: 255

        public int PageOffSet { get; set; }
    }
}