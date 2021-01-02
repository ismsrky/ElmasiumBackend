namespace Mh.Business.Bo.Report
{
    public class ReportPersonSummaryBo
    {
        public decimal SaleGrandTotal { get; set; }
        public decimal SaleCostTotal { get; set; }

        public decimal RevenueCashTotal { get; set; }
        public decimal RevenueBankTotal { get; set; }
        public decimal ChargeSaleTotal { get; set; }

        public decimal CreditTotal { get; set; }
        public decimal DebtTotal { get; set; }

        public decimal PersonAccountCashTotal { get; set; }
        public decimal PersonAccountBankTotal { get; set; }
        public decimal PersonAccountCreditCardTotal { get; set; }
    }
}