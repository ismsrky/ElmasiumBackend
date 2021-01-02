using Mh.Business.Bo.Fiche.Money;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Fiche;
using Mh.Service.Dto.Fiche.Money;
using Mh.Service.Dto.Sys;
using System.Collections.Generic;
using System.Web.Http;

namespace Mh.Service.WebApi.Fiche
{
    public class FicheMoneyController : BaseController
    {
        readonly IFicheMoneyBusiness ficheMoneyBusiness;
        public FicheMoneyController(IFicheMoneyBusiness _ficheMoneyBusiness)
        {
            ficheMoneyBusiness = _ficheMoneyBusiness;
        }

        [HttpPost]        
        public ResponseDto<List<FicheMoneyListDto>> GetList(FicheMoneyGetListCriteriaDto criteriaDto)
        {
            FicheMoneyGetListCriteriaBo criteriaBo = new FicheMoneyGetListCriteriaBo()
            {
                FicheId = criteriaDto.FicheId,

                Session = Session
            };

            ResponseBo<List<FicheMoneyListBo>> responseBo = ficheMoneyBusiness.GetList(criteriaBo);

            ResponseDto<List<FicheMoneyListDto>> responseDto = responseBo.ToResponseDto<List<FicheMoneyListDto>, List<FicheMoneyListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<FicheMoneyListDto>();
                foreach (FicheMoneyListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new FicheMoneyListDto()
                    {
                        Id = itemBo.Id,
                        Total = itemBo.Total,

                        DebtPersonAccountId = itemBo.DebtPersonAccountId,
                        DebtPersonAccountName = itemBo.DebtPersonAccountName,

                        CreditPersonAccountId = itemBo.CreditPersonAccountId,
                        CreditPersonAccountName = itemBo.CreditPersonAccountName,

                        DebtPersonAccountTypeId = itemBo.DebtPersonAccountTypeId,
                        CreditPersonAccountTypeId = itemBo.CreditPersonAccountTypeId,

                        Notes = itemBo.Notes
                    });
                }
            }

            return responseDto;
        }
    }
}