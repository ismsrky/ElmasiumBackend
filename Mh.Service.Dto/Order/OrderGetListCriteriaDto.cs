using System.Collections.Generic;

namespace Mh.Service.Dto.Order
{
    public class OrderGetListCriteriaDto
    {
        public int CaseId { get; set; } // 0: Normal, 1: Top 10 and 'GetIncomings', 'GetReturns', 'OrderStatList', 'PageOffSet' params are ignored.

        public long PersonId { get; set; }

        public bool GetIncomings { get; set; }
        public bool GetReturns { get; set; }
        public List<Enums.OrderStats> OrderStatList { get; set; }

        public Enums.Currencies CurrencyId { get; set; }

        public int PageOffSet { get; set; }
    }
}