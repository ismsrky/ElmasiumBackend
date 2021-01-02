using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Tools.ImageCompress
{
    public class ImageProcess
    {
        public long ProductImageId { get; set; }
        public Guid UniqueId { get; set; }
        public StoreType StoreTypeId { get; set; }
        public Enums.ImageTypes ImageTypeId { get; set; }
        public Enums.FileTypes FileTypeId { get; set; }

        public bool Done { get; set; }
    }

    public enum StoreType
    {
        Normal = 0,
        Thumbnail = 1
    }
}