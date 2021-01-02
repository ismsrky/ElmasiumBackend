using Mh.Business.Contract.Dictionary;
using Mh.Business.Contract.Person;
using Mh.Service.Dto.Dictionary;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mh.Service.WebApi.Dictionary
{
    public class DictionaryController : BaseController
    {
        readonly IDictionaryBusiness dictionaryBusiness;
        readonly IRealPersonBusiness realPersonBusiness;
        public DictionaryController(IDictionaryBusiness _dictionaryBusiness, IRealPersonBusiness _realPersonBusiness)
        {
            dictionaryBusiness = _dictionaryBusiness;
            realPersonBusiness = _realPersonBusiness;
        }

        /*
         [HttpPost]
        
        public ResponseDto<List<DictionaryDto>> GetList(DictionaryGetListDto dictionaryGetListDto)
        {
            ResponseDto<List<DictionaryDto>> responseDto = new ResponseDto<List<DictionaryDto>>()
            {
                IsSuccess = true,
                Dto = new List<DictionaryDto>()
            };

            Enums.Languages langId = Session.RealPerson.LanguageId;

            if (Business.Stc.NeedToSendDics(dictionaryGetListDto.ChangeSetID, dictionaryGetListDto.LanguageId))
            {
                responseDto.Dto = new List<DictionaryDto>();

                foreach (DicItem item in Business.Stc.DicItemList)
                {
                    responseDto.Dto.Add(new DictionaryDto()
                    {
                        Key = item.Key,
                        Value = Business.Stc.GetbyLang(item, dictionaryGetListDto.LanguageId)
                    });
                }
            }

            //if user decides to change language in login screen, then we change it
            if (dictionaryGetListDto.LanguageId != Session.RealPerson.LanguageId)
            {
                personBusiness.ChangeLanguage(Session.RealPerson.Id, dictionaryGetListDto.LanguageId);
            }

            return responseDto;
        }
             */

        [AllowAnonymous]
        [HttpPost]
        public ResponseDto<List<DictionaryDto>> GetList(DictionaryGetListCriteriaDto criteriaDto)
        {
            ResponseDto<List<DictionaryDto>> responseDto = new ResponseDto<List<DictionaryDto>>();
            responseDto.IsSuccess = true;

            if (Business.Stc.NeedToSendDics(criteriaDto.ChangeSetID, criteriaDto.LanguageId))
            {
                responseDto.Dto = new List<DictionaryDto>();

                foreach (DicItem item in Business.Stc.DicItemList.Where(x => x.IsForDesign))
                {
                    responseDto.Dto.Add(new DictionaryDto()
                    {
                        Key = item.Key,
                        Value = Business.Stc.GetbyLang(item, criteriaDto.LanguageId)
                    });
                }
            }

            return responseDto;
        }
    }
}