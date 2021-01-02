namespace Mh.Business.Bo.Address
{
    public class AddressStateBo
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; } // In native language. No need to be translated.
    }
}