using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Product.Category
{
    public class ProductCategoryGetListCriteriaBo : BaseBo
    {
        public int ProductCategoryId { get; set; }
        public bool IsUpper { get; set; }
    }
}