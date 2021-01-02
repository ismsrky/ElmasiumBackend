using Mh.Business.Bo.Product;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Product
{
    public interface IProductBusiness
    {
        ResponseBo Save(ProductSaveBo saveBo);
        ResponseBo SaveStar(ProductStarBo saveBo);

        ResponseBo Update(ProductUpdateBo updateBo);
        ResponseBo UpdateCheck(ProductUpdateCheckBo updateCheckBo);

        ResponseBo GetNextId();
    }
}