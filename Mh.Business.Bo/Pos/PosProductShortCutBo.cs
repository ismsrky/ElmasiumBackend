using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Pos
{
    public class PosProductShortCutBo : BaseBo
    {
        public long Id { get; set; }
        public long ShopId { get; set; }
        public long ProductId { get; set; }
        public long GroupId { get; set; }
    }
}