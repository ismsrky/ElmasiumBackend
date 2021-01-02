using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Relation
{
    public class PersonRelationHasCriteriaBo : BaseBo
    {
        public Enums.RelationTypes RelationTypeId { get; set; }

        public long PersonId1 { get; set; }
        public long? PersonId2 { get; set; }
    }
}