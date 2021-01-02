using System;

namespace Mh.Db.Tables
{
    public class Fiche
    {
        public int Id { get; set; } // int, not null
        public int DebtPersonId { get; set; } // int, not null
        public int CreditPersonId { get; set; } // int, not null
        public int FicheTypeId { get; set; } // int, not null
        public string Code { get; set; } // nvarchar(50), not null
        public DateTime IssueDate { get; set; } // date, not null
        public decimal GrandTotal { get; set; } // decimal(18,2), not null
        public decimal Total { get; set; } // decimal(18,2), not null
        public decimal RowDiscountTotal { get; set; } // decimal(18,2), not null
        public decimal UnderDiscountTotal { get; set; } // decimal(18,2), not null
        public decimal VatTotal { get; set; } // decimal(18,2), not null
        public string Notes { get; set; } // nvarchar(250), null
    }
}
