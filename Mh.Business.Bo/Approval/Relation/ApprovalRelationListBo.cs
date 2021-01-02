using System;

namespace Mh.Business.Bo.Approval.Relation
{
    public class ApprovalRelationListBo
    {
        public long ApprovalRelationId { get; set; }

        public long ParentPersonId { get; set; }
        public Enums.PersonTypes ParentPersonTypeId { get; set; }
        public string ParentPersonFullName { get; set; }


        public long ChildPersonId { get; set; }
        public Enums.PersonTypes ChildPersonTypeId { get; set; }
        public string ChildPersonFullName { get; set; }

        public Enums.RelationTypes RelationTypeId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}