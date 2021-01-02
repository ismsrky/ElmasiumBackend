using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Pos
{
    public class PosProductShortCutGroupBo : BaseBo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ShopId { get; set; }
    }
}