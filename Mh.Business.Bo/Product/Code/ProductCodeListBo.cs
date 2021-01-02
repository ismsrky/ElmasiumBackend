using System;

namespace Mh.Business.Bo.Product.Code
{
    public class ProductCodeListBo
    {
        public long Id { get; set; }

        public string Code { get; set; }
        public Enums.ProductCodeTypes ProductCodeTypeId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}