namespace Mh.Business.Bo.Person.Shop
{
    public class ShopAddressBo
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