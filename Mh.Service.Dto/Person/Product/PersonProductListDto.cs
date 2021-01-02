using Mh.Service.Dto.Product.Code;
using Mh.Service.Dto.Product.Price;
using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Product
{
    public class PersonProductListDto
    {
        public long Id { get; set; } // Person product id

        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }

        public decimal VateRate { get; set; }

        public decimal Balance { get; set; }

        public ProductPriceDto Price { get; set; }

        public string PortraitImageUniqueIdStr { get; set; }

        public List<ProductCodeDto> CodeList { get; set; } // not null. At least one row that stock code.
    }
}