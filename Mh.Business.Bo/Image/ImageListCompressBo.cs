using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Bo.Image
{
    public class ImageListCompressBo
    {
        public long Id { get; set; }
        public Guid UniqueId { get; set; }
        public Enums.ImageTypes ImageTypeId { get; set; }
        public Enums.FileTypes FileTypeId { get; set; }

        public bool IsCompressed { get; set; }
        public bool IsCompressedThumbnail { get; set; }
    }
}