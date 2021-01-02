namespace Mh.Service.Dto.Order
{
    public class OrderReturnSaveDto
    {
        public long OrderId { get; set; }
        public string Notes { get; set; } // not null. max length: 255
    }
}