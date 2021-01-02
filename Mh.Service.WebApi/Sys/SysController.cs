using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Sys;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System.Web.Http;

namespace Mh.Service.WebApi.Sys
{
    public class SysController : BaseController
    {
        readonly ISysBusiness sysBusiness;
        public SysController(ISysBusiness _sysBusiness)
        {
            sysBusiness = _sysBusiness;
        }

        [HttpPost]
        public ResponseDto<SysVersionDto> GetLatestVersion(SysVersionGetLatestCriteriaDto criteriaDto)
        {
            SysVersionGetLatestCriteriaBo criteriaBo = new SysVersionGetLatestCriteriaBo()
            {
                ApplicationTypeId = criteriaDto.ApplicationTypeId,

                Session = Session
            };

            ResponseBo<SysVersionBo> responseBo = sysBusiness.GetLatestVersion(criteriaBo);

            ResponseDto<SysVersionDto> responseDto = responseBo.ToResponseDto<SysVersionDto, SysVersionBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new SysVersionDto()
                {
                    Id = responseBo.Bo.Id,
                    ApplicationTypeId = responseBo.Bo.ApplicationTypeId,
                    Version = responseBo.Bo.Version,
                    ReleaseDateNumber = responseBo.Bo.ReleaseDate.ToNumberFromDateTimeNull(),
                    ReleaseNote = responseBo.Bo.ReleaseNote
                };
            }

            return responseDto;
        }
    }
}