using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Approval.Relation
{
    public class ApprovalRelationGetListCriteriaBo : BaseBo
    {
        public long MyPersonId { get; set; }
        public bool GetIncomings { get; set; }
    }
}