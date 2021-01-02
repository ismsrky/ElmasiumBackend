using Mh.Business.Bo.EnumsOp;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.EnumsOp
{
    public interface IEnumsOpBusiness
    {
        ResponseBo<List<CurrenciesBo>> GetCurrencyList();
        ResponseBo<List<ShopTypeBo>> GetShopTypeList();

        ResponseBo<List<FicheContentBo>> GetFicheContentList();
    }
}