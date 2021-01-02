using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Approval.Relation
{
    public class ApprovalRelationSaveBo : BaseBo
    {
        // One of following params must be passed.
        public long? ApprovalRelationId { get; set; }
        public long? PersonRelationId { get; set; }
        ///////////////////////////////////

        public Enums.ApprovalStats ApprovalStatId { get; set; }
    }
}