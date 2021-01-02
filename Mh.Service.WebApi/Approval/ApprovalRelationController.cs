using Mh.Business.Bo.Approval.Relation;
using Mh.Business.Bo.Person.Real;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Approval;
using Mh.Service.Dto.Approval.Relation;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System.Collections.Generic;
using System.Web.Http;

namespace Mh.Service.WebApi.Approval
{
    public class ApprovalRelationController : BaseController
    {
        readonly IApprovalRelationBusiness approvalRelationBusiness;
        public ApprovalRelationController(IApprovalRelationBusiness _approvalRelationBusiness)
        {
            approvalRelationBusiness = _approvalRelationBusiness;
        }

        [HttpPost]
        public ResponseDto MakeRequest(ApprovalRelationRequestDto requestDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ApprovalRelationRequestBo requestBo = new ApprovalRelationRequestBo()
            {
                ChildRelationTypeId = requestDto.ChildRelationTypeId,

                ParentPersonId = requestDto.ParentPersonId,
                ChildPersonId = requestDto.ChildPersonId,

                Session = Session
            };

            ResponseBo responseBo = approvalRelationBusiness.MakeRequest(requestBo);

            base.SendNotifyWsToList(responseBo.PersonNotifyList);

            responseDto = responseBo.ToResponseDto();
            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<ApprovalRelationListDto>> GetList(ApprovalRelationGetListCriteriaDto criteriaDto)
        {
            ApprovalRelationGetListCriteriaBo criteriaBo = new ApprovalRelationGetListCriteriaBo()
            {
                MyPersonId = criteriaDto.MyPersonId,
                GetIncomings = criteriaDto.GetIncomings,

                Session = Session
            };

            ResponseBo<List<ApprovalRelationListBo>> responseBo = approvalRelationBusiness.GetList(criteriaBo);

            ResponseDto<List<ApprovalRelationListDto>> responseDto = responseBo.ToResponseDto<List<ApprovalRelationListDto>, List<ApprovalRelationListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<ApprovalRelationListDto>();
                foreach (ApprovalRelationListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new ApprovalRelationListDto()
                    {
                        ApprovalRelationId = itemBo.ApprovalRelationId,

                        ParentPersonId = itemBo.ParentPersonId,
                        ParentPersonTypeId = itemBo.ParentPersonTypeId,
                        ParentPersonFullName = itemBo.ParentPersonFullName,

                        ChildPersonId = itemBo.ChildPersonId,
                        ChildPersonTypeId = itemBo.ChildPersonTypeId,
                        ChildPersonFullName = itemBo.ChildPersonFullName,

                        RelationTypeId = itemBo.RelationTypeId,

                        CreateDateNumber = itemBo.CreateDate.ToNumberFromDateTime()
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Save(ApprovalRelationSaveDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ApprovalRelationSaveBo saveBo = new ApprovalRelationSaveBo()
            {
                ApprovalRelationId = saveDto.ApprovalRelationId,
                PersonRelationId = saveDto.PersonRelationId,
                ApprovalStatId = saveDto.ApprovalStatId,

                Session = Session
            };

            ResponseBo responseBo = approvalRelationBusiness.Save(saveBo);

            base.UpdateMyPersonIdList();

            base.SendNotifyWsToList(responseBo.PersonNotifyList);

            responseDto = responseBo.ToResponseDto();
            return responseDto;
        }
    }
}