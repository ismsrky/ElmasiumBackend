using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Product
{
    public class PersonProductCategoryGetListCriteriaBo : BaseBo
    {
        public Enums.ProductTypes ProductTypeId { get; set; }
        public long PersonId { get; set; }

        public bool? IsSaleForOnline { get; set; }
        public bool? IsTemporarilyUnavaible { get; set; }

        public Enums.StockStats? StockStatId { get; set; }

        public string ProductNameCode { get; set; } // null. max lenght: 255
    }
}