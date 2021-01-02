using Mh.Business.Bo.Fiche.Money;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Fiche
{
    public interface IFicheMoneyBusiness
    {
        ResponseBo<List<FicheMoneyListBo>> GetList(FicheMoneyGetListCriteriaBo criteriaBo);
    }
}