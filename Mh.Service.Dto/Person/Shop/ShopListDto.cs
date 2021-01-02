namespace Mh.Service.Dto.Person.Shop
{
    public class ShopListDto
    {
        public long Id { get; set; }
        public string ShortName { get; set; } //Sign name of the shop

        public Enums.ShopTypes ShopTypeId { get; set; }
        public Enums.EnumStats StatId { get; set; }

        public string AddressCityName { get; set; }
        public string AddressDistrictName { get; set; }
        public string AddressLocalityName { get; set; }
        public string AddressQuarterName { get; set; }
        public string AddressNotes { get; set; }
        public string AddressPhone { get; set; }
        public string AddressInvolvedPersonName { get; set; }
    }
}