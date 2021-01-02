using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Product.Category
{
    public class ProductCategoryListBo : BaseBo
    {
        public int Id { get; set; }
        public string Name { get; set; } // No need to be translated.

        public string UrlName { get; set; } // Comes value of field 'Id' if not specified.

        public int ParentId { get; set; }

        public bool IsLast { get; set; }
    }
}