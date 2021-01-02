namespace Mh.Business.Bo.Fiche
{
    public class FicheVatTotalBo
    {
        public long Id { get; set; }
        public long FicheId { get; set; }

        public decimal VatRate { get; set; }
        public decimal VatTotal { get; set; }
    }
}