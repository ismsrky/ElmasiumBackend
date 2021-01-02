namespace Mh.Service.Dto.Person.Shop
{
    public class ShopAddressDto
    {
        public long ShopId { get; set; }

        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int LocalityId { get; set; }
        public int QuarterId { get; set; }
        public string Notes { get; set; }
        public string Phone { get; set; }
        public string InvolvedPersonName { get; set; }
    }
}