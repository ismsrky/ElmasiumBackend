using System;

namespace Mh.Service.Dto.Order.StatHistory
{
    public class OrderStatHistoryListDto
    {
        public long Id { get; set; }
        public Enums.OrderStats OrderStatId { get; set; }

        public long ParentPersonId { get; set; }
        public string ParentPersonFullName { get; set; }

        public string Notes { get; set; }
        public double CreateDateNumber { get; set; }
    }
}