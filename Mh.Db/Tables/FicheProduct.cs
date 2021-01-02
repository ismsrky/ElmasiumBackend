using System;

namespace Mh.Db.Tables
{
    public class FicheProduct
    {
        public int Id { get; set; } // int, not null
        public int FicheId { get; set; } // int, not null
        public int ProductId { get; set; } // int, not null
        public int DebtPersonId { get; set; } // int, not null
        public int CreditPersonId { get; set; } // int, not null
        public decimal Amount { get; set; } // decimal(18,2), not null
        public decimal UnitPrice { get; set; } // decimal(18,2), not null
        public decimal Total { get; set; } // decimal(18,2), not null
        public decimal Discount { get; set; } // decimal(18,2), not null
        public decimal VatRate { get; set; } // decimal(18,2), not null
        public decimal VatTotal { get; set; } // decimal(18,2), not null
        public decimal GrandTotal { get; set; } // decimal(18,2), not null
    }
}
