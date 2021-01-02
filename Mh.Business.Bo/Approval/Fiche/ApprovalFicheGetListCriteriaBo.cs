using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Approval.Fiche
{
    public class ApprovalFicheGetListCriteriaBo : BaseBo
    {
        public long MyPersonId { get; set; }
        public bool GetIncomings { get; set; }
    }
}