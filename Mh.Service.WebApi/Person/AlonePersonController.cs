using Mh.Business.Bo.Person.Alone;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Service.Dto.Person.Alone;
using Mh.Service.Dto.Sys;
using System.Web.Http;

namespace Mh.Service.WebApi.Person
{
    public class AlonePersonController : BaseController
    {
        readonly IAlonePersonBusiness alonePersonBusiness;

        public AlonePersonController(IAlonePersonBusiness _alonePersonBusiness)
        {
            alonePersonBusiness = _alonePersonBusiness;
        }

        [HttpPost]        
        public ResponseDto<AlonePersonDto> Get(AlonePersonGetCriteriaDto criteriaDto)
        {
            AlonePersonGetCriteriaBo criteriaBo = new AlonePersonGetCriteriaBo()
            {
                PersonId = criteriaDto.PersonId,
                ParentRelationPersonId = criteriaDto.ParentRelationPersonId,

                Session = Session
            };

            ResponseBo<AlonePersonBo> responseBo = alonePersonBusiness.Get(criteriaBo);

            ResponseDto<AlonePersonDto> responseDto = responseBo.ToResponseDto<AlonePersonDto, AlonePersonBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new AlonePersonDto()
                {
                    Id = responseBo.Bo.Id,
                    Name = responseBo.Bo.Name,
                    Surname = responseBo.Bo.Surname,
                    Email = responseBo.Bo.Email,

                    PersonTypeId = responseBo.Bo.PersonTypeId,

                    StatId = responseBo.Bo.StatId,
                    DefaultCurrencyId = responseBo.Bo.DefaultCurrencyId,

                    Phone = responseBo.Bo.Phone,
                    Notes = responseBo.Bo.Notes,

                    ParentRelationPersonId = responseBo.Bo.ParentRelationPersonId,
                    ChildRelationTypeId = responseBo.Bo.ChildRelationTypeId,

                    TaxOffice = responseBo.Bo.TaxOffice,
                    TaxNumber = responseBo.Bo.TaxNumber,

                    PersonAddressId = responseBo.Bo.PersonAddressId,
                    AddressCountryId = responseBo.Bo.AddressCountryId,
                    AddressStateId = responseBo.Bo.AddressStateId,
                    AddressCityId = responseBo.Bo.AddressCityId,
                    AddressDistrictId = responseBo.Bo.AddressDistrictId,
                    AddressNotes = responseBo.Bo.Notes
                };
            }

            return responseDto;
        }


        [HttpPost]        
        public ResponseDto Save(AlonePersonDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            AlonePersonBo personAccountBo = new AlonePersonBo()
            {
                Id = saveDto.Id,
                Name = saveDto.Name,
                Surname = saveDto.Surname,
                Email = saveDto.Email,

                PersonTypeId = saveDto.PersonTypeId,

                StatId = saveDto.StatId,
                DefaultCurrencyId = saveDto.DefaultCurrencyId,


                Phone = saveDto.Phone,
                Notes = saveDto.Notes,

                ParentRelationPersonId = saveDto.ParentRelationPersonId,
                ChildRelationTypeId = saveDto.ChildRelationTypeId,

                TaxNumber = saveDto.TaxNumber,
                TaxOffice = saveDto.TaxOffice,

                PersonAddressId = saveDto.PersonAddressId,
                AddressCountryId = saveDto.AddressCountryId,
                AddressStateId = saveDto.AddressStateId,
                AddressCityId = saveDto.AddressCityId,
                AddressDistrictId = saveDto.AddressDistrictId,
                AddressNotes = saveDto.AddressNotes,

                Session = Session
            };

            ResponseBo responseBo = alonePersonBusiness.Save(personAccountBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }
    }
}