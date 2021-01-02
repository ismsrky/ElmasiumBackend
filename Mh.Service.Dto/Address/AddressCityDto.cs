namespace Mh.Service.Dto.Address
{
    public class AddressCityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Plate { get; set; }

        public int? StatedId { get; set; }
        public int CountryId { get; set; }
    }
}