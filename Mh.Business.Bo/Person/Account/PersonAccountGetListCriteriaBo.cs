using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Bo.Person.Account
{
    public class PersonAccountGetListCriteriaBo : BaseBo
    {
        public long OwnerPersonId { get; set; }

        public List<Enums.AccountTypes> AccountTypeIdList { get; set; }
        public Enums.EnumStats? StatId { get; set; }
        public Enums.Currencies? CurrencyId { get; set; }
    }
}