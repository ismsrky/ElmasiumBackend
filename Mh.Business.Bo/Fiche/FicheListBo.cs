using System;

namespace Mh.Business.Bo.Fiche
{
    public class FicheListBo
    {
        public long Id { get; set; }

        public long DebtPersonId { get; set; }
        public string DebtPersonFullName { get; set; }
        public Enums.PersonTypes DebtPersonTypeId { get; set; }
        public bool DebtPersonIsAlone { get; set; }

        public long CreditPersonId { get; set; }
        public string CreditPersonFullName { get; set; }
        public Enums.PersonTypes CreditPersonTypeId { get; set; }
        public bool CreditPersonIsAlone { get; set; }

        public Enums.FicheTypes FicheTypeId { get; set; }
        public Enums.Currencies CurrencyId { get; set; }
        public Enums.ApprovalStats ApprovalStatId { get; set; }
        public bool IncludingVat { get; set; }

        public Enums.PaymentStats PaymentStatId { get; set; }
        public decimal PaidTotal { get; set; }

        public Enums.FicheContents FicheContentId { get; set; }
        public Enums.FicheContentGroups FicheContentGroupId { get; set; }

        public string PrintedCode { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? DueDate { get; set; }

        public decimal GrandTotal { get; set; }
        public decimal Total { get; set; }
        public decimal RowDiscountTotal { get; set; }

        public decimal UnderDiscountRate { get; set; }
        public decimal UnderDiscountTotal { get; set; }

        public Enums.FicheTypeFakes FicheTypeFakeId { get; set; }
        public string Notes { get; set; }

        public int IsDebtor { get; set; }

        public long? LastApprovalFicheHistoryParentPersonId { get; set; }
        public long? LastApprovalFicheHistoryChildPersonId { get; set; }
        public Enums.ApprovalStats? LastApprovalStatId { get; set; }
    }
}