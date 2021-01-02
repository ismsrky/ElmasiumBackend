namespace Mh.Service.Dto.Person.Product
{
    public class PersonProductActivityListDto
    {
        public long Id { get; set; }
        public decimal Quantity { get; set; }

        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }
        public bool IsDebt { get; set; } // debt or credit. No other options avabile.

        public long OwnerPersonId { get; set; } // Person whose owner this product.

        public long FicheId { get; set; } // which fiche does this activity belong?
        public Enums.Currencies FicheCurrencyId { get; set; }
        public Enums.ApprovalStats FicheApprovalStatId { get; set; }
        public double FicheIssueDateNumber { get; set; }
    }
}