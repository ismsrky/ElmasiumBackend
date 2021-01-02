using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Approval.Relation
{
    public class ApprovalRelationRequestBo : BaseBo
    {
        public Enums.RelationTypes ChildRelationTypeId { get; set; }

        public long ParentPersonId { get; set; }
        public long ChildPersonId { get; set; }
    }
}