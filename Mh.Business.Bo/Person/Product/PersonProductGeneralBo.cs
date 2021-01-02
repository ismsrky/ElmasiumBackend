namespace Mh.Business.Bo.Person.Product
{
    public class PersonProductGeneralBo
    {
        public Enums.Currencies DefaultCurrencyId { get; set; }

        public decimal PurchaseVatRate { get; set; }
        public decimal SaleVatRate { get; set; }

        public decimal PurhasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal OnlineSalePrice { get; set; }

        public bool IsTemporarilyUnavaible { get; set; }
        public bool IsSaleForOnline { get; set; }
        public string Notes { get; set; }
    }
}