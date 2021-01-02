namespace Mh.Service.Dto.Person.Product
{
    public class PersonProductGetListCriteriaDto
    {
        public string ProductNameCode { get; set; }
        public Enums.ProductTypes? ProductTypeId { get; set; } // null means all.

        public Enums.StockStats? StockStatId { get; set; } // null means all.

        public long PersonId { get; set; } // can be real or shop.
        public Enums.Currencies CurrencyId { get; set; } // Price will be in this currency.

        public int PageOffSet { get; set; }
    }
}