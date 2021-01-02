namespace Mh.Service.Dto.Person
{
    public class PersonChangeMyPersonDto
    {
        public long MyPersonId { get; set; }
        public long PersonRelationId { get; set; }

        public Enums.Currencies DefaultCurrencyId { get; set; }
    }
}