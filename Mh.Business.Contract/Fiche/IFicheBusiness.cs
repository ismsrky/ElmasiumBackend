using Mh.Business.Bo.Fiche;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Fiche
{
    public interface IFicheBusiness
    {
        ResponseBo Save(FicheBo ficheBo);

        ResponseBo<FicheBo> Get(FicheGetCriteriaBo criteriaBo);

        ResponseBo<List<FicheListBo>> GetList(FicheGetListCriteriaBo criteriaBo);

        ResponseBo Delete(FicheDeleteBo deleteBo);

        ResponseBo<List<FicheApprovalHistoryListBo>> GetApprovalHistoryList(FicheApprovalHistoryGetListCriteriaBo criteriaBo);

        ResponseBo GetNextId();

        ResponseBo<FicheActionsBo> GetActions(FicheGetActionsCriteriaBo criteriaBo);
    }
}