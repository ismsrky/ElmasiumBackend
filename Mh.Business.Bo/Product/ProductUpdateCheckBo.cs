using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Product
{
    public class ProductUpdateCheckBo : BaseBo
    {
        public long ProductId { get; set; }

        public Enums.ProductUpdateTypes ProductUpdateTypeId { get; set; }
    }
}