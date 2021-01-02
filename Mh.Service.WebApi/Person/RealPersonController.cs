using Mh.Business.Bo.Person.Real;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Service.Dto.Dictionary;
using Mh.Service.Dto.Person.Real;
using Mh.Service.Dto.Sys;
using Mh.Sessions;
using Mh.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace Mh.Service.WebApi.Person
{
    public class RealPersonController : BaseController
    {
        readonly IRealPersonBusiness realPersonBusiness;

        public RealPersonController(IRealPersonBusiness _realPersonBusiness)
        {
            realPersonBusiness = _realPersonBusiness;
        }

        [HttpPost]
        
        public ResponseDto<RealPersonDto> Get()
        {
            // This method is just for logged-in users.
            ResponseBo<RealPersonBo> responseBo = realPersonBusiness.Get(base.ToBaseBo());

            ResponseDto<RealPersonDto> responseDto = responseBo.ToResponseDto<RealPersonDto, RealPersonBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new RealPersonDto()
                {
                    Name = responseBo.Bo.Name,
                    Surname = responseBo.Bo.Surname,
                    Username = responseBo.Bo.Username,
                    Email = responseBo.Bo.Email,
                    BirthdateNumber = responseBo.Bo.Birthdate.ToNumberFromDateTimeNull(),
                    GenderId = responseBo.Bo.GenderId,
                    DefaultCurrencyId = responseBo.Bo.DefaultCurrencyId,
                    Phone = responseBo.Bo.Phone
                };
            }

            return responseDto;
        }

        [HttpPost]
        
        public ResponseDto Update(RealPersonDto realPersonDto)
        {
            ResponseDto responseDto = new ResponseDto();

            RealPersonBo realPersonBo = new RealPersonBo()
            {
                Id = Session.RealPerson.Id,
                Name = realPersonDto.Name,
                Surname = realPersonDto.Surname,
                Birthdate = realPersonDto.BirthdateNumber.ToDateTimeFromNumberNull(),
                GenderId = realPersonDto.GenderId,
                DefaultCurrencyId = realPersonDto.DefaultCurrencyId,
                Phone = realPersonDto.Phone,

                Session = Session
            };

            responseDto = realPersonBusiness.Update(realPersonBo).ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        
        public ResponseDto<List<DictionaryDto>> ChangeLanguage(DictionaryGetListCriteriaDto criteriaDto)
        {
            ResponseDto<List<DictionaryDto>> responseDto = new ResponseDto<List<DictionaryDto>>();

            BaseBo baseBo = base.ToBaseBo();
            baseBo.Session.RealPerson.LanguageId = criteriaDto.LanguageId;

            // Anonymous persons cannot effect language in db.
            if (baseBo.Session.RealPerson.Id != -2)
            {
                ResponseBo responseBo = realPersonBusiness.ChangeLanguage(baseBo);
                responseDto = responseBo.ToResponseDto<List<DictionaryDto>>();

                if (responseBo.IsSuccess)
                {
                    foreach (Session session in SessionManager.SessionList)
                    {
                        if (session.RealPerson.Id != Session.RealPerson.Id) continue;
                        session.RealPerson.LanguageId = criteriaDto.LanguageId;
                    }

                    if (Business.Stc.NeedToSendDics(criteriaDto.ChangeSetID, criteriaDto.LanguageId))
                    {
                        responseDto.Dto = new List<DictionaryDto>();
                        foreach (DicItem item in Business.Stc.DicItemList)
                        {
                            responseDto.Dto.Add(new DictionaryDto()
                            {
                                Key = item.Key,
                                Value = Business.Stc.GetbyLang(item, criteriaDto.LanguageId)
                            });
                        }
                    }
                }
            }
            else
            {
                responseDto.IsSuccess = true;

                if (Business.Stc.NeedToSendDics(criteriaDto.ChangeSetID, criteriaDto.LanguageId))
                {
                    responseDto.Dto = new List<DictionaryDto>();
                    foreach (DicItem item in Business.Stc.DicItemList)
                    {
                        responseDto.Dto.Add(new DictionaryDto()
                        {
                            Key = item.Key,
                            Value = Business.Stc.GetbyLang(item, criteriaDto.LanguageId)
                        });
                    }
                }
            }

            return responseDto;
        }
    }
}