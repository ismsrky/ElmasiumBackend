using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Bo.Person.Product
{
    public class PersonProductUpdateBo : BaseBo
    {
        public long PersonProductId { get; set; }
        public List<Enums.ProductUpdateTypes> ProductUpdateTypeList { get; set; } // not null. At least one type is required.

        // value(s) to be updated. //
        // A value must not null or null if it is presented in 'ProductUpdateTypeList' otherwise it is ignored.

        public string Name { get; set; } // not null it is presented.

        public int? CategoryId { get; set; } // not null it is presented.

        public decimal? PurchaseVatRate { get; set; } // not null it is presented.
        public decimal? SaleVatRate { get; set; } // not null it is presented.

        public decimal? PurhasePrice { get; set; } // not null it is presented.
        public decimal? SalePrice { get; set; } // not null it is presented.
        public decimal? OnlineSalePrice { get; set; } // not null it is presented.

        public bool? IsTemporarilyUnavaible { get; set; } // not null it is presented.
        public bool? IsSaleForOnline { get; set; } // not null it is presented.
        public string Notes { get; set; } // not null it is presented. this is person product note, not product note.
        // value(s) to be updated. //
    }
}