using Mh.Business.Bo.Report;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Report;
using Mh.Service.Dto.Report;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mh.Service.WebApi.Report
{
    public class ReportPersonController : BaseController
    {
        readonly IReportPersonBusiness reportPersonBusiness;
        public ReportPersonController(IReportPersonBusiness _reportPersonBusiness)
        {
            reportPersonBusiness = _reportPersonBusiness;
        }

        [HttpPost]
        public ResponseDto<ReportPersonSummaryDto> GetSummary(ReportPersonSummaryGetCriteriaDto criteriaDto)
        {
            ReportPersonSummaryGetCriteriaBo criteriaBo = new ReportPersonSummaryGetCriteriaBo()
            {
                IssueDateStart = criteriaDto.IssueDateStartNumber.ToDateTimeFromNumber(),
                IssueDateEnd = criteriaDto.IssueDateEndNumber.ToDateTimeFromNumber(),

                CurrencyId = criteriaDto.CurrencyId,

                Session = Session
            };

            ResponseBo<ReportPersonSummaryBo> responseBo = reportPersonBusiness.GetSummary(criteriaBo);

            ResponseDto<ReportPersonSummaryDto> responseDto = responseBo.ToResponseDto<ReportPersonSummaryDto, ReportPersonSummaryBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new ReportPersonSummaryDto();
                responseDto.Dto.SaleGrandTotal = responseBo.Bo.SaleGrandTotal;
                responseDto.Dto.SaleCostTotal = responseBo.Bo.SaleCostTotal;

                responseDto.Dto.CreditTotal = responseBo.Bo.CreditTotal;
                responseDto.Dto.DebtTotal = responseBo.Bo.DebtTotal;

                responseDto.Dto.RevenueCashTotal = responseBo.Bo.RevenueCashTotal;
                responseDto.Dto.RevenueBankTotal = responseBo.Bo.RevenueBankTotal;
                responseDto.Dto.ChargeSaleTotal = responseBo.Bo.ChargeSaleTotal;

                responseDto.Dto.PersonAccountCashTotal = responseBo.Bo.PersonAccountCashTotal;
                responseDto.Dto.PersonAccountBankTotal = responseBo.Bo.PersonAccountBankTotal;
                responseDto.Dto.PersonAccountCreditCardTotal = responseBo.Bo.PersonAccountCreditCardTotal;
            }

            return responseDto;
        }
    }
}