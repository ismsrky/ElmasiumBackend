namespace Mh.Business.Bo.Product.Price
{
    public class ProductPriceBo
    {
        public decimal PurhasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? OnlineSalePrice { get; set; }
        public Enums.Currencies CurrencyId { get; set; }

        public bool FromPool { get; set; }
    }
}