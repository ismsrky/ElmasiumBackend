namespace Mh.Service.Dto.Person.Product
{
    public class PersonProductAddInventoryDto
    {
        public long ProductId { get; set; }
        public long PersonId { get; set; } // can be real or shop.
    }
}