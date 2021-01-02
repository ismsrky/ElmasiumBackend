using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Account
{
    public class PersonAccountGetFastRetailCriteriaBo : BaseBo
    {
        public Enums.Currencies CurrencyId { get; set; }
        public Enums.AccountTypes AccountTypeId { get; set; }
    }
}