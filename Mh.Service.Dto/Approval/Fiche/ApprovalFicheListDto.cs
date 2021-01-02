namespace Mh.Service.Dto.Approval.Fiche
{
    public class ApprovalFicheListDto
    {
        public long ApprovalFicheId { get; set; }

        public long DebtPersonId { get; set; }
        public Enums.PersonTypes DebtPersonTypeId { get; set; }
        public string DebtPersonFullName { get; set; }

        public long CreditPersonId { get; set; }
        public Enums.PersonTypes CreditPersonTypeId { get; set; }
        public string CreditPersonFullName { get; set; }

        public long FicheId { get; set; }
        public Enums.FicheTypes FicheTypeId { get; set; }
        public decimal FicheGrandTotal { get; set; }
        public Enums.Currencies FicheCurrencyId { get; set; }
        public Enums.ApprovalStats FicheApprovalStatId { get; set; }

        public Enums.FicheTypeFakes FicheTypeFakeId { get; set; }

        public bool HasRelation { get; set; }

        public double CreateDateNumber { get; set; }
    }
}