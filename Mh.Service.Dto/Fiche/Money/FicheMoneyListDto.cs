namespace Mh.Service.Dto.Fiche.Money
{
    public class FicheMoneyListDto
    {
        public long Id { get; set; }
        public decimal Total { get; set; }

        public long? DebtPersonAccountId { get; set; }
        public string DebtPersonAccountName { get; set; }

        public long? CreditPersonAccountId { get; set; }
        public string CreditPersonAccountName { get; set; }

        public Enums.AccountTypes DebtPersonAccountTypeId { get; set; }
        public Enums.AccountTypes CreditPersonAccountTypeId { get; set; }

        public string Notes { get; set; }
    }
}