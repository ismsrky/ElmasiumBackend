namespace Mh.Service.Dto.Person.Relation
{
    public class PersonRelationSubListDto
    {
        public long PersonRelationId { get; set; }
        public Enums.ApprovalStats ApprovalStatId { get; set; }

        public Enums.RelationTypes RelationTypeId { get; set; }

        public long? ApprovalRelationId { get; set; }
    }
}