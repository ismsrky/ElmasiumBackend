using Mh.Service.Dto.Product.Code;
using Mh.Service.Dto.Product.Price;
using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Product
{
    public class PersonProductDto
    {
        public long Id { get; set; }
        public decimal PurchaseVatRate { get; set; }
        public decimal SaleVatRate { get; set; }
        public int? CategoryId { get; set; }
        public decimal Balance { get; set; }

        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }

        public List<ProductCodeDto> CodeList { get; set; }
        public ProductPriceDto Price { get; set; }

        public string PortraitImageUniqueIdStr { get; set; }
    }
}