using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Account
{
    public class PersonAccountBo : BaseBo
    {
        public long Id { get; set; }
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