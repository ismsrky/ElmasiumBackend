using Mh.Business.Bo.Product.Category;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Product
{
    public interface IProductCategoryBusiness
    {
        ResponseBo<List<ProductCategoryListBo>> GetList(ProductCategoryGetListCriteriaBo criteriaBo);
        ResponseBo<List<ProductCategoryListAdminBo>> GetListAdmin(ProductCategoryGetListAdminCriteriaBo criteriaBo);

        ResponseBo Save(ProductCategoryListBo saveBo);
        ResponseBo Delete(ProductCategoryListBo deleteBo);

        ResponseBo<ProductCategoryCheckUrlBo> CheckUrl(ProductCategoryCheckUrlCriteriaBo criteriaBo);
    }
}