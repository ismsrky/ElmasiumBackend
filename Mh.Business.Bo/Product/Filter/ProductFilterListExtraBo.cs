using System;

namespace Mh.Business.Bo.Product.Filter
{
    public class ProductFilterListExtraBo
    {
        public long PersonProductId { get; set; }

        public Guid? PortraitImageUniqueId { get; set; }
        public Enums.FileTypes? PortraitImageFileTypeId { get; set; }
    }
}