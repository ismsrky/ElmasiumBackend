using System;

namespace Mh.Business.Bo.Order.StatHistory
{
    public class OrderStatHistoryListBo
    {
        public long Id { get; set; }
        public Enums.OrderStats OrderStatId { get; set; }

        public long ParentPersonId { get; set; }
        public string ParentPersonFullName { get; set; }

        public string Notes { get; set; }
        public DateTime CreateDate { get; set; }
    }
}