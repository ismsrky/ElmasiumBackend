using Mh.Service.Dto.Fiche.Money;
using System.Collections.Generic;

namespace Mh.Service.Dto.Approval.Fiche
{
    public class ApprovalFicheSaveDto
    {
        public long FicheId { get; set; }
        public Enums.ApprovalStats ApprovalStatId { get; set; }

        public List<FicheMoneyDto> ChoiceReturnList { get; set; }
    }
}