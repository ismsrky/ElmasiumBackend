namespace Mh.Service.Dto.Fiche.Money
{
    public class FicheMoneyDto
    {
        public long Id { get; set; }
        public long FicheId { get; set; }

        public long? DebtPersonAccountId { get; set; }
        public long? CreditPersonAccountId { get; set; }

        public decimal Total { get; set; }

        public Enums.AccountTypes DebtPersonAccountTypeId { get; set; }
        public Enums.AccountTypes CreditPersonAccountTypeId { get; set; }

        public string Notes { get; set; }
    }
}