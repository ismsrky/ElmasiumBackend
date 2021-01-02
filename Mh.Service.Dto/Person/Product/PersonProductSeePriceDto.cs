using Mh.Service.Dto.Product.Code;
using Mh.Service.Dto.Product.Price;
using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Product
{
    public class PersonProductSeePriceDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }

        public List<ProductCodeDto> CodeList { get; set; }
        public ProductPriceDto Price { get; set; }

        public string PortraitImageUniqueIdStr { get; set; }

        public long? PersonProductId { get; set; }
    }
}