using Mh.Business.Bo.Person.VerifyPhone;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Service.Dto.Person.VerifyPhone;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System.Web.Http;

namespace Mh.Service.WebApi.Person
{
    public class PersonVerifyPhoneController : BaseController
    {
        readonly IPersonVerifyPhoneBusiness personVerifyPhoneBusiness;

        public PersonVerifyPhoneController(IPersonVerifyPhoneBusiness _personVerifyPhoneBusiness)
        {
            personVerifyPhoneBusiness = _personVerifyPhoneBusiness;
        }

        [HttpPost]
        public ResponseDto<PersonVerifyPhoneGenReturnDto> Gen(PersonVerifyPhoneGenDto genDto)
        {
            PersonVerifyPhoneGenBo genBo = new PersonVerifyPhoneGenBo()
            {
                PersonId = genDto.PersonId,
                Phone = genDto.Phone,

                Session = Session
            };

            ResponseBo<PersonVerifyPhoneGenReturnBo> responseBo = personVerifyPhoneBusiness.Gen(genBo);

            ResponseDto<PersonVerifyPhoneGenReturnDto> responseDto = responseBo.ToResponseDto<PersonVerifyPhoneGenReturnDto, PersonVerifyPhoneGenReturnBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new PersonVerifyPhoneGenReturnDto()
                {
                    StartDateTimeNumber = responseBo.Bo.StartDateTime.ToNumberFromDateTime(),
                    EndDateTimeNumber = responseBo.Bo.EndDateTime.ToNumberFromDateTime(),

                    CountDownInSeconds = responseBo.Bo.CountDownInSeconds
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Save(PersonVerifyPhoneSaveDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonVerifyPhoneSaveBo saveBo = new PersonVerifyPhoneSaveBo()
            {
                Id = saveDto.Id,
                VerifyCode = saveDto.VerifyCode,

                Session = Session
            };

            ResponseBo responseBo = personVerifyPhoneBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto IsVerified(PersonVerifyPhoneGenDto genDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonVerifyPhoneGenBo genBo = new PersonVerifyPhoneGenBo()
            {
                PersonId = genDto.PersonId,
                Phone = genDto.Phone,

                Session = Session
            };

            ResponseBo responseBo = personVerifyPhoneBusiness.IsVerified(genBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }
    }
}