using Mh.Business.Bo.Fiche.Money;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Bo.Approval.Fiche
{
    public class ApprovalFicheSaveBo : BaseBo
    {
        public long FicheId { get; set; }
        public Enums.ApprovalStats ApprovalStatId { get; set; }

        public List<FicheMoneyBo> ChoiceReturnList { get; set; }
    }
}