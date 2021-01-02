namespace Mh.Business.Bo.Person.Account
{
    public class PersonAccountListBo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Enums.AccountTypes AccountTypeId { get; set; }
        public Enums.Currencies CurrencyId { get; set; }
        public Enums.EnumStats StatId { get; set; }
        public decimal Balance { get; set; }

        public bool IsFastRetail { get; set; }
    }
}