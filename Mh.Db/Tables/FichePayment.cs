using System;

namespace Mh.Db.Tables
{
    public class FichePayment
    {
        public int Id { get; set; } // int, not null
        public int FicheId { get; set; } // int, not null
        public int DebtPersonAccountId { get; set; } // int, not null
        public int CreditPersonAccountId { get; set; } // int, not null
        public decimal Total { get; set; } // decimal(18,2), not null
    }
}
