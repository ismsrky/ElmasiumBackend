namespace Mh.Service.Dto.Person.Product
{
    public class PersonProductSeePriceGetCriteriaDto
    {
        // One of followings must be filled.
        public long? ProductId { get; set; }
        public string ProductCode { get; set; }

        public long ShopId { get; set; }

        public Enums.Currencies CurrencyId { get; set; }
    }
}