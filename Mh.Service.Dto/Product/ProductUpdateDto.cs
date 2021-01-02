using System;

namespace Mh.Service.Dto.Product
{
    public class ProductUpdateDto
    {
        public long ProductId { get; set; }

        public Enums.ProductUpdateTypes ProductUpdateTypeId { get; set; }

        public string Name { get; set; }
        
        public int? OriginCountryId { get; set; }
        public string Notes { get; set; }
        public int CategoryId { get; set; }

        public Guid? ImageUniqueId { get; set; }
        public Enums.FileTypes? ImageFileTypeId { get; set; }
    }
}