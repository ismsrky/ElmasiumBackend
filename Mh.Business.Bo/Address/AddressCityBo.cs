namespace Mh.Business.Bo.Address
{
    public class AddressCityBo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Plate { get; set; }

        public int? StatedId { get; set; }
        public int CountryId { get; set; }
    }
}