using Mh.Business.Bo.Approval.Fiche;
using Mh.Business.Bo.Fiche;
using Mh.Business.Bo.Fiche.Money;
using Mh.Business.Bo.Person.Real;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Approval;
using Mh.Service.Dto.Approval.Fiche;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mh.Service.WebApi.Approval
{
    public class ApprovalFicheController : BaseController
    {
        readonly IApprovalFicheBusiness approvalFicheBusiness;
        public ApprovalFicheController(IApprovalFicheBusiness _approvalFicheBusiness)
        {
            approvalFicheBusiness = _approvalFicheBusiness;
        }

        [HttpPost]
        public ResponseDto<List<ApprovalFicheListDto>> GetList(ApprovalFicheGetListCriteriaDto criteriaDto)
        {
            ApprovalFicheGetListCriteriaBo criteriaBo = new ApprovalFicheGetListCriteriaBo()
            {
                MyPersonId = criteriaDto.MyPersonId,
                GetIncomings = criteriaDto.GetIncomings,

                Session = Session
            };

            ResponseBo<List<ApprovalFicheListBo>> responseBo = approvalFicheBusiness.GetList(criteriaBo);

            ResponseDto<List<ApprovalFicheListDto>> responseDto = responseBo.ToResponseDto<List<ApprovalFicheListDto>, List<ApprovalFicheListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<ApprovalFicheListDto>();
                foreach (ApprovalFicheListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new ApprovalFicheListDto()
                    {
                        ApprovalFicheId = itemBo.ApprovalFicheId,

                        DebtPersonId = itemBo.DebtPersonId,
                        DebtPersonTypeId = itemBo.DebtPersonTypeId,
                        DebtPersonFullName = itemBo.DebtPersonFullName,

                        CreditPersonId = itemBo.CreditPersonId,
                        CreditPersonTypeId = itemBo.CreditPersonTypeId,
                        CreditPersonFullName = itemBo.CreditPersonFullName,

                        FicheId = itemBo.FicheId,
                        FicheTypeId = itemBo.FicheTypeId,
                        FicheGrandTotal = itemBo.FicheGrandTotal,
                        FicheCurrencyId = itemBo.FicheCurrencyId,
                        FicheApprovalStatId = itemBo.FicheApprovalStatId,

                        FicheTypeFakeId = itemBo.FicheTypeFakeId,

                        HasRelation = itemBo.HasRelation,

                        CreateDateNumber = itemBo.CreateDate.ToNumberFromDateTime()
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Save(ApprovalFicheSaveDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ApprovalFicheSaveBo saveBo = new ApprovalFicheSaveBo()
            {
                FicheId = saveDto.FicheId,
                ApprovalStatId = saveDto.ApprovalStatId,

                Session = Session
            };

            if (saveDto.ChoiceReturnList != null && saveDto.ChoiceReturnList.Count > 0)
            {
                saveBo.ChoiceReturnList = (from x in saveDto.ChoiceReturnList
                                                        select new FicheMoneyBo
                                                        {
                                                            Id = x.Id,
                                                            DebtPersonAccountId = x.DebtPersonAccountId,
                                                            CreditPersonAccountId = x.CreditPersonAccountId,
                                                            Total = x.Total,
                                                            DebtPersonAccountTypeId = x.DebtPersonAccountTypeId,
                                                            CreditPersonAccountTypeId = x.CreditPersonAccountTypeId,
                                                            Notes = x.Notes
                                                        }).ToList();
            }

            ResponseBo responseBo = approvalFicheBusiness.Save(saveBo);

            base.SendNotifyWsToList(responseBo.PersonNotifyList);

            responseDto = responseBo.ToResponseDto();
            return responseDto;
        }       
    }
}