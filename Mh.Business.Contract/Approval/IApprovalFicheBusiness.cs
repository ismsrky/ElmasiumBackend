using Mh.Business.Bo.Approval.Fiche;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Approval
{
    public interface IApprovalFicheBusiness
    {
        ResponseBo<List<ApprovalFicheListBo>> GetList(ApprovalFicheGetListCriteriaBo criteriaBo);

        ResponseBo Save(ApprovalFicheSaveBo saveBo);
    }
}