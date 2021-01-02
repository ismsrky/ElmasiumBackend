namespace Mh.Service.Dto.Fiche.Product
{
    public class FicheProductDto
    {
        public long Id { get; set; }
        public long FicheId { get; set; }
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal VatRate { get; set; }
        public decimal VatTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }
    }
}