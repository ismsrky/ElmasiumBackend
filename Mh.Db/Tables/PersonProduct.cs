using System;

namespace Mh.Db.Tables
{
    public class PersonProduct
    {
        public int Id { get; set; } // int, not null
        public int PersonId { get; set; } // int, not null
        public int ProductId { get; set; } // int, not null
        public int ProductSnycTypeId { get; set; } // int, not null
        public decimal PurhasePrice { get; set; } // decimal(18,2), not null
        public decimal SalePrice { get; set; } // decimal(18,2), not null
        public decimal VateRate { get; set; } // decimal(18,2), not null
        public decimal Balance { get; set; } // decimal(18,2), not null
    }
}
