using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Account
{
    public class PersonAccountGetListCriteriaDto
    {
        public long OwnerPersonId { get; set; }

        public List<Enums.AccountTypes> AccountTypeIdList { get; set; }
        public Enums.EnumStats? StatId { get; set; }
        public Enums.Currencies? CurrencyId { get; set; }
    }
}