using System.Collections.Generic;

namespace Mh.Service.Dto.Product.Category
{
    public class ProductCategoryListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } // raw and needs to be translated.

        public string UrlName { get; set; }

        public bool IsLast { get; set; }

        public int ParentId { get; set; }
    }
}