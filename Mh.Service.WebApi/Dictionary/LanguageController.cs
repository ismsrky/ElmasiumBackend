using Mh.Business.Bo.Dictionary;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Dictionary;
using Mh.Service.Dto.Dictionary;
using Mh.Service.Dto.Sys;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Mh.Service.WebApi.Dictionary
{
    public class LanguageController : BaseController
    {
        readonly ILanguageBusiness languageBusiness;

        public LanguageController(ILanguageBusiness _languageBusiness)
        {
            languageBusiness = _languageBusiness;
        }

        [HttpPost]
        [AllowAnonymous]
        
        public ResponseDto<List<LanguageDto>> GetList()
        {
            ResponseBo<List<LanguageBo>> responseBo = languageBusiness.GetList();

            ResponseDto<List<LanguageDto>> responseDto = responseBo.ToResponseDto<List<LanguageDto>, List<LanguageBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<LanguageDto>();
                LanguageDto lang = null;
                foreach (LanguageBo item in responseBo.Bo)
                {
                    lang = new LanguageDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        CultureCode = item.CultureCode
                    };

                    responseDto.Dto.Add(lang);
                }
            }

            return responseDto;
        }
    }
}