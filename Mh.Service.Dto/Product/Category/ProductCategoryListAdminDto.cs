using System.Collections.Generic;

namespace Mh.Service.Dto.Product.Category
{
    public class ProductCategoryListAdminDto
    {
        public int Id { get; set; }
        public string Name { get; set; } // No need to be translated.

        public int ParentId { get; set; }

        public List<ProductCategoryListAdminDto> SubCategoryList { get; set; }
    }
}