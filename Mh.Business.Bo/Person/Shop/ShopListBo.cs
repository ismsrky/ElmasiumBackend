namespace Mh.Business.Bo.Person.Shop
{
    public class ShopListBo
    {
        public int Id { get; set; }
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