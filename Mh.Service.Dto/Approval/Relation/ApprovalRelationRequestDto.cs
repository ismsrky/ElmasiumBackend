namespace Mh.Service.Dto.Approval.Relation
{
    public class ApprovalRelationRequestDto
    {
        public Enums.RelationTypes ChildRelationTypeId { get; set; }

        public long ParentPersonId { get; set; }
        public long ChildPersonId { get; set; }
    }
}