using Mh.Business.Bo.Sys;
using System;

namespace Mh.Business.Bo.Image
{
    public class ImageBo : BaseBo
    {
        public Enums.ImageTypes ImageTypeId { get; set; }
        public Guid UniqueId { get; set; }
        public Enums.FileTypes FileTypeId { get; set; }

        public long? ProductId { get; set; }
        public long? PersonId { get; set; }
        public long? PersonProductId { get; set; }
    }
}