namespace Mh.Service.Dto.Order.StatHistory
{
    public class OrderStatHistoryDto
    {
        public long Id { get; set; }

        public long OrderId { get; set; }
        public Enums.OrderStats OrderStatId { get; set; }

        public long PersonId { get; set; }

        public string Notes { get; set; } // null or not null due to table 'OrderStatNext'.

        public Enums.AccountTypes? AccountTypeId { get; set; } // null or not null due to table 'EnumAccountType'.

        public double CreateDateNumber { get; set; }
    }
}