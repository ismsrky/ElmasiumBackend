namespace Mh.Service.Dto.Product.Price
{
    public class ProductPriceDto
    {
        public decimal PurhasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? OnlineSalePrice { get; set; }
        public Enums.Currencies CurrencyId { get; set; }

        public bool FromPool { get; set; }
    }
}