using Mh.Business.Bo.Image;
using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Contract.Image
{
    public interface IImageBusiness
    {
        ResponseBo<List<ImageListCompressBo>> GetListCompress();
        ResponseBo MarkAsCompressed(long id, bool isThumbnail);

        ResponseBo Save(ImageBo saveBo);
    }
}