namespace Mh.Service.Dto.Order
{
    public class OrderStatNextListDto
    {
        public int Id { get; set; }

        public Enums.OrderStats OrderStatId { get; set; }
        public Enums.OrderStats NextOrderStatId { get; set; }

        public bool ForDebt { get; set; }
        public bool ForReturn { get; set; }
    }
}