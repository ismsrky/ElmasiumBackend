using Mh.Business.Bo.Product.Code;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Product
{
    public interface IProductCodeBusiness
    {
        ResponseBo<List<ProductCodeListBo>> GetList(ProductCodeGetListCriteriaBo criteriaBo);

        ResponseBo Save(ProductCodeBo saveBo);
    }
}