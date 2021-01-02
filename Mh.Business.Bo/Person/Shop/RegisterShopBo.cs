using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Shop
{
    public class RegisterShopBo : BaseBo
    {
        public string ShortName { get; set; } //Sign name of the shop

        public Enums.ShopTypes ShopTypeId { get; set; }
        public Enums.Currencies DefaultCurrencyId { get; set; }
    }
}