namespace Mh.Service.Dto.Address
{
    public class AddressStateDto
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; } // In native language. No need to be translated.
    }
}