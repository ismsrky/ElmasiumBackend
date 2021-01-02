using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Relation.Find
{
    public class PersonRelationFindGetListCriteriaDto
    {
        public long ParentPersonId { get; set; }
        public string Name { get; set; }
        public Enums.RelationTypes RelationTypeId { get; set; }

        public long OperatorRealId { get; set; } // Who requests?
        public Enums.Languages LanguageId { get; set; }
    }
}