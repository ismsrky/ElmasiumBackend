using Mh.Business.Bo.Fiche;
using Mh.Business.Bo.Fiche.Product;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Fiche
{
    public interface IFicheProductBusiness
    {
        ResponseBo<List<FicheProductListBo>> GetList(FicheProductGetListCriteriaBo criteriaBo);

        ResponseBo UpdateProducts(FicheBo ficheBo);
    }
}