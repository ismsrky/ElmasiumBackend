namespace Mh.Service.Dto.Approval.Relation
{
    public class ApprovalRelationListDto
    {
        public long ApprovalRelationId { get; set; }

        public long ParentPersonId { get; set; }
        public Enums.PersonTypes ParentPersonTypeId { get; set; }
        public string ParentPersonFullName { get; set; }


        public long ChildPersonId { get; set; }
        public Enums.PersonTypes ChildPersonTypeId { get; set; }
        public string ChildPersonFullName { get; set; }

        public Enums.RelationTypes RelationTypeId { get; set; }

        public double CreateDateNumber { get; set; }
    }
}