namespace Mh.Service.Dto.Person.Address
{
    public class PersonAddressDto
    {
        public long Id { get; set; }
        public Enums.AddressTypes AddressTypeId { get; set; }
        public long PersonId { get; set; }
        public string InvolvedPersonName { get; set; }
        public Enums.EnumStats StatId { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? LocalityId { get; set; }

        public string ZipCode { get; set; }
        public string Notes { get; set; }
        public string Phone { get; set; }
    }
}