namespace Mh.Service.Dto.Fiche
{
    public class FicheVatTotalDto
    {
        public long Id { get; set; }
        public long FicheId { get; set; }

        public decimal VatRate { get; set; }
        public decimal VatTotal { get; set; }
    }
}