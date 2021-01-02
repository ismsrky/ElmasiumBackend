namespace Mh.Service.Dto.Approval.Relation
{
    public class ApprovalRelationSaveDto
    {
        // One of following params must be passed.
        public long? ApprovalRelationId { get; set; }
        public long? PersonRelationId { get; set; }
        ///////////////////////////////////
        ///
        public Enums.ApprovalStats ApprovalStatId { get; set; }
    }
}