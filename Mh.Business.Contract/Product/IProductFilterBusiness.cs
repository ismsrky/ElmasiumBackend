using Mh.Business.Bo.Product.Filter;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Product
{
    public interface IProductFilterBusiness
    {
        ResponseBo<List<ProductFilterListBo>> GetList(ProductFilterGetListCriteriaBo criteriaBo);
        ResponseBo<ProductFilterListExtraBo> GetListExtra(ProductFilterGetListExtraCriteriaBo criteriaBo);

        ResponseBo<ProductFilterListSummaryBo> GetListSummary(ProductFilterGetListCriteriaBo criteriaBo);

        ResponseBo<List<ProductFilterPoolListBo>> GetPoolList(ProductFilterPoolGetListCriteriaBo criteriaBo);
    }
}