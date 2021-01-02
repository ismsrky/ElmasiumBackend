using Mh.Service.Dto.Product.Code;
using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Product
{
    public class PersonProfileProductListDto
    {
        public long Id { get; set; }

        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }

        public decimal OnlineSalePrice { get; set; }

        public int StarCount { get; set; }
        public int StarSum { get; set; }

        public decimal SaleVatRate { get; set; }
        public decimal Balance { get; set; }
        public bool IsSaleForOnline { get; set; }
        public bool IsTemporarilyUnavaible { get; set; }
        public string Notes { get; set; }

        public string PortraitImageUniqueIdStr { get; set; }        
    }
}