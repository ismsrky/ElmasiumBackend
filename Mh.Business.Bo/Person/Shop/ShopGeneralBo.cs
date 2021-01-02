using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Shop
{
    public class ShopGeneralBo : BaseBo
    {
        public long ShopId { get; set; }

        public string FullName { get; set; } //Full name of the shop
        public string ShortName { get; set; } //Sign name of the shop

        public Enums.ShopTypes ShopTypeId { get; set; }

        public bool IsIncludingVatPurhasePrice { get; set; }
        public bool IsIncludingVatSalePrice { get; set; }

        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }

        public Enums.Currencies DefaultCurrencyId { get; set; }

        public string UrlName { get; set; }

        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
    }
}