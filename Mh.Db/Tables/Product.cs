using System;

namespace Mh.Db.Tables
{
    public class Product
    {
        public int Id { get; set; } // int, not null
        public string Name { get; set; } // nvarchar(250), not null
        public int ProductTypeId { get; set; } // int, not null
        public decimal PurhasePrice { get; set; } // decimal(18,2), not null
        public decimal SalePrice { get; set; } // decimal(18,2), not null
        public decimal VateRate { get; set; } // decimal(18,2), not null
    }
}
