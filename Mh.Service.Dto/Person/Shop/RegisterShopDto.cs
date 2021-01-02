namespace Mh.Service.Dto.Person.Shop
{
    public class RegisterShopDto
    {
        public string ShortName { get; set; } //Sign name of the shop

        public Enums.ShopTypes ShopTypeId { get; set; }
        public Enums.Currencies DefaultCurrencyId { get; set; }
    }
}