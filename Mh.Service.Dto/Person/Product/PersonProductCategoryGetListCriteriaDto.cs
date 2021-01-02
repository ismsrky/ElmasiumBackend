namespace Mh.Service.Dto.Person.Product
{
    public class PersonProductCategoryGetListCriteriaDto
    {
        public Enums.ProductTypes ProductTypeId { get; set; }
        public long PersonId { get; set; }

        public bool? IsSaleForOnline { get; set; }
        public bool? IsTemporarilyUnavaible { get; set; }

        public Enums.StockStats? StockStatId { get; set; }

        public string ProductNameCode { get; set; } // null. max lenght: 255
    }
}