namespace Mh.Service.Dto.Person.Address
{
    public class PersonAddressListDto
    {
        public int Id { get; set; }
        public Enums.AddressTypes AddressTypeId { get; set; }
        public Enums.EnumStats StatId { get; set; }
        public string Name { get; set; }
        public string InvolvedPersonName { get; set; }

        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string LocalityName { get; set; }

        public string ZipCode { get; set; }
        public string Notes { get; set; }
        public string Phone { get; set; }
    }
}