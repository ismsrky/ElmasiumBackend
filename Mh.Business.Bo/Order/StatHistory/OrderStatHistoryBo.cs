using Mh.Business.Bo.Sys;
using System;

namespace Mh.Business.Bo.Order.StatHistory
{
    public class OrderStatHistoryBo : BaseBo
    {
        public long Id { get; set; }

        public long OrderId { get; set; }
        public Enums.OrderStats OrderStatId { get; set; }

        public long PersonId { get; set; }

        public string Notes { get; set; } // null or not null due to table 'OrderStatNext'.

        public Enums.AccountTypes? AccountTypeId { get; set; } // null or not null due to table 'EnumAccountType'.

        public DateTime CreateDate { get; set; }
    }
}