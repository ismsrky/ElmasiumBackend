using System.Collections.Generic;

namespace Mh.Service.Dto.Person
{
    public class PersonGetListCriteriaDto
    {
        public List<Enums.PersonTypes> PersonTypeIdList { get; set; }
        public Enums.Currencies CurrencyId { get; set; }
    }
}