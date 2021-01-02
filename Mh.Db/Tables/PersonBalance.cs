using System;

namespace Mh.Db.Tables
{
    public class PersonBalance
    {
        public int Id { get; set; } // int, not null
        public int DebtPersonId { get; set; } // int, not null
        public int CreditPersonId { get; set; } // int, not null
        public decimal Balance { get; set; } // decimal(18,2), not null
    }
}
