namespace Mh.Business.Bo.Product.Category
{
    public class ProductCategoryListAdminBo
    {
        public int Id { get; set; }
        public string Name { get; set; } // No need to be translated.

        public int ParentId { get; set; }
    }
}