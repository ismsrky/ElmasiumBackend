using Mh.Business.Bo.Approval.Relation;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Approval
{
    public interface IApprovalRelationBusiness
    {
        ResponseBo MakeRequest(ApprovalRelationRequestBo requestBo);

        ResponseBo<List<ApprovalRelationListBo>> GetList(ApprovalRelationGetListCriteriaBo criteriaBo);

        ResponseBo Save(ApprovalRelationSaveBo saveBo);
    }
}