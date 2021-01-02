using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Bo.Person
{
    public class PersonGetListCriteriaBo : BaseBo
    {
        public List<Enums.PersonTypes> PersonTypeIdList { get; set; }
        public Enums.Currencies CurrencyId { get; set; }
    }
}