using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Order
{
    public class OrderReturnSaveBo : BaseBo
    {
        public long OrderId { get; set; }
        public string Notes { get; set; } // not null. max length: 255
    }
}