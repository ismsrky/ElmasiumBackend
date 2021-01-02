namespace Mh.Service.Dto.Product
{
    public class ProductSaveDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }

        public long? PersonId { get; set; } // A record of 'PersonProduct' will be inserted if any value presented. Can be real or shop.

        public decimal SalePrice { get; set; }
        public decimal PurhasePrice { get; set; }
        public Enums.Currencies CurrencyId { get; set; }
        public decimal VatRate { get; set; }

        public string Barcode { get; set; } // The value is ignored if 'ProductTypeId' is not 0. And it must be unique.
    }
}