namespace Mh.Service.Dto.Person.Relation
{
    public class PersonRelationHasCriteriaDto
    {
        public Enums.RelationTypes RelationTypeId { get; set; }

        public long PersonId1 { get; set; }
        public long? PersonId2 { get; set; }
    }
}