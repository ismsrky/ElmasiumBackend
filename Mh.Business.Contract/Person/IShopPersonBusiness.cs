using Mh.Business.Bo.Person.Shop;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Person
{
    public interface IShopPersonBusiness
    {
        ResponseBo Register(RegisterShopBo registerBo);

        ResponseBo FullNameExists(string fullName, Enums.Languages languageId);

        ResponseBo UpdateGeneral(ShopGeneralBo shopGeneralBo);
        ResponseBo<ShopGeneralBo> GetGeneral(ShopGeneralGetCriteriaBo criteriaBo);

        ResponseBo UpdateWorkingHours(ShopWorkingHoursBo updateBo);

        ResponseBo<ShopWorkingHoursBo> GetWorkingHours(ShopWorkingHoursGetCriteriaBo criteriaBo);

        ResponseBo<ShopProfileBo> GetProfile(ShopProfileGetCriteriaBo criteriaBo);

        ResponseBo UpdateOrderGeneral(ShopOrderGeneralBo updateBo);
        ResponseBo<ShopOrderGeneralBo> GetOrderGeneral(ShopOrderGeneralGetCriteriaBo criteriaBo);

        ResponseBo SaveOrderArea(ShopOrderAreaBo saveBo);
        ResponseBo<List<ShopOrderAreaListBo>> GetOrderAreaList(ShopOrderAreaGetListCriteriaBo criteriaBo);
        ResponseBo<ShopOrderAreaBo> GetOrderArea(ShopOrderAreaGetCriteriaBo criteriaBo);
        ResponseBo DeleteOrderArea(ShopOrderAreaDeleteBo deleteBo);
        ResponseBo DeleteOrderAreaSub(ShopOrderAreaSubDeleteBo deleteBo);

        ResponseBo<List<ShopOrderAccountListBo>> GetOrderAccountList(ShopOrderAccountGetListCriteriaBo criteriaBo);
    }
}