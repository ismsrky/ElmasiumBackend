namespace Mh.Business.Bo.Order
{
    public class OrderStatNextListBo
    {
        public int Id { get; set; }

        public Enums.OrderStats OrderStatId { get; set; }
        public Enums.OrderStats NextOrderStatId { get; set; }

        public bool ForDebt { get; set; }
        public bool ForReturn { get; set; }
    }
}