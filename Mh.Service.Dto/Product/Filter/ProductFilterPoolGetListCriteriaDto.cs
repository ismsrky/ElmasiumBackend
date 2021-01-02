namespace Mh.Service.Dto.Product.Filter
{
    public class ProductFilterPoolGetListCriteriaDto
    {
        public string ProductNameCode { get; set; }

        public Enums.ProductTypes? ProductTypeId { get; set; }
        public int? ProductCategoryId { get; set; }

        public bool OnlyInInventory { get; set; }
        public bool OnlyInStock { get; set; }

        public long PersonId { get; set; } // can be real or shop.

        public int PageOffSet { get; set; }
    }
}