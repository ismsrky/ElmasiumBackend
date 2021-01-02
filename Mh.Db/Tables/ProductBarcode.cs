using System;

namespace Mh.Db.Tables
{
    public class ProductBarcode
    {
        public int Id { get; set; } // int, not null
        public int ProductId { get; set; } // int, not null
        public string Barcode { get; set; } // nvarchar(50), not null
    }
}
