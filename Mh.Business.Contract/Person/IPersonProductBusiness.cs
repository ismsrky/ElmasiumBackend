using Mh.Business.Bo.Person.Product;
using Mh.Business.Bo.Product.Category;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Person
{
    public interface IPersonProductBusiness
    {
        ResponseBo<PersonProductBo> Get(PersonProductGetCriteriaBo criteriaBo);

        ResponseBo Delete(PersonProductDeleteBo criteriaBo);

        ResponseBo<List<PersonProductListBo>> GetList(PersonProductGetListCriteriaBo criteriaBo);

        ResponseBo<PersonProductGeneralBo> GetGeneral(PersonProductGeneralGetCriteriaBo criteriaBo);

        ResponseBo AddToInventory(PersonProductAddInventoryBo addBo);
        ResponseBo Update(PersonProductUpdateBo updateBo);

        ResponseBo<List<PersonProductActivityListBo>> GetActivityList(PersonProductActivityGetListCriteriaBo criteriaBo);

        ResponseBo<PersonProductProfileBo> GetProfile(PersonProductProfileGetCriteriaBo criteriaBo);

        ResponseBo<PersonProductSeePriceBo> GetSeePrice(PersonProductSeePriceGetCriteriaBo criteriaBo);

        ResponseBo<List<ProductCategoryListBo>> GetCategoryList(PersonProductCategoryGetListCriteriaBo criteriaBo);
        ResponseBo<List<PersonProfileProductListBo>> GetListForProfile(PersonProfileProductGetListCriteriaBo criteriaBo);
    }
}