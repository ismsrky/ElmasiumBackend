using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Product.Code
{
    public class ProductCodeBo : BaseBo
    {
        public string Code { get; set; }
        public Enums.ProductCodeTypes ProductCodeTypeId { get; set; }

        public long ProductId { get; set; }
    }
}