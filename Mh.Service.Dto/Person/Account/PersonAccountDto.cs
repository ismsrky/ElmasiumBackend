namespace Mh.Service.Dto.Person.Account
{
    public class PersonAccountDto
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string Name { get; set; }
        public Enums.AccountTypes AccountTypeId { get; set; }
        public Enums.Currencies CurrencyId { get; set; }
        public Enums.EnumStats StatId { get; set; }
        public bool IsDefault { get; set; }
        public decimal Balance { get; set; }
        public string Notes { get; set; }

        public bool IsFastRetail { get; set; }
    }
}