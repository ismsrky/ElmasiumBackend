using Mh.Business.Bo.Person.Address;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Service.Dto.Person.Address;
using Mh.Service.Dto.Sys;
using System.Collections.Generic;
using System.Web.Http;

namespace Mh.Service.WebApi.Person
{
    public class PersonAddressController : BaseController
    {
        readonly IPersonAddressBusiness personAddressBusiness;

        public PersonAddressController(IPersonAddressBusiness _personAddressBusiness)
        {
            personAddressBusiness = _personAddressBusiness;
        }

        [HttpPost]
        public ResponseDto<PersonAddressDto> Get(PersonAddressGetCriteriaDto criteriaDto)
        {
            PersonAddressGetCriteriaBo criteriaBo = new PersonAddressGetCriteriaBo()
            {
                AddressId = criteriaDto.AddressId,

                Session = Session
            };

            ResponseBo<PersonAddressBo> responseBo = personAddressBusiness.Get(criteriaBo);

            ResponseDto<PersonAddressDto> responseDto = responseBo.ToResponseDto<PersonAddressDto, PersonAddressBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new PersonAddressDto()
                {
                    Id = responseBo.Bo.Id,
                    AddressTypeId = responseBo.Bo.AddressTypeId,
                    PersonId = responseBo.Bo.PersonId,
                    InvolvedPersonName = responseBo.Bo.InvolvedPersonName,
                    StatId = responseBo.Bo.StatId,
                    Name = responseBo.Bo.Name,

                    CountryId = responseBo.Bo.CountryId,
                    StateId = responseBo.Bo.StateId,
                    CityId = responseBo.Bo.CityId,
                    DistrictId = responseBo.Bo.DistrictId,
                    LocalityId = responseBo.Bo.LocalityId,

                    ZipCode = responseBo.Bo.ZipCode,
                    Notes = responseBo.Bo.Notes,
                    Phone = responseBo.Bo.Phone
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<PersonAddressListDto>> GetList(PersonAddressGetListCriteriaDto criteriaDto)
        {
            PersonAddressGetListCriteriaBo criteriaBo = new PersonAddressGetListCriteriaBo()
            {
                OwnerPersonId = criteriaDto.OwnerPersonId,

                AddressTypeIdList = criteriaDto.AddressTypeIdList,
                StatId = criteriaDto.StatId,

                AddressIdList = criteriaDto.AddressIdList,

                Session = Session
            };

            ResponseBo<List<PersonAddressListBo>> responseBo = personAddressBusiness.GetList(criteriaBo);

            ResponseDto<List<PersonAddressListDto>> responseDto = responseBo.ToResponseDto<List<PersonAddressListDto>, List<PersonAddressListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PersonAddressListDto>();
                foreach (PersonAddressListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new PersonAddressListDto()
                    {
                        Id = itemBo.Id,
                        AddressTypeId = itemBo.AddressTypeId,
                        StatId = itemBo.StatId,
                        Name = itemBo.Name,
                        InvolvedPersonName = itemBo.InvolvedPersonName,

                        CountryName = itemBo.CountryName,
                        StateName = itemBo.StateName,
                        CityName = itemBo.CityName,
                        DistrictName = itemBo.DistrictName,
                        LocalityName = itemBo.LocalityName,

                        ZipCode = itemBo.ZipCode,
                        Notes = itemBo.Notes,
                        Phone = itemBo.Phone
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Save(PersonAddressDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonAddressBo personAddressBo = new PersonAddressBo()
            {
                Id = saveDto.Id,
                AddressTypeId = saveDto.AddressTypeId,
                PersonId = saveDto.PersonId,
                InvolvedPersonName = saveDto.InvolvedPersonName,
                StatId = saveDto.StatId,
                Name = saveDto.Name,

                CountryId = saveDto.CountryId,
                StateId = saveDto.StateId,
                CityId = saveDto.CityId,
                DistrictId = saveDto.DistrictId,
                LocalityId = saveDto.LocalityId,

                ZipCode = saveDto.ZipCode,
                Notes = saveDto.Notes,
                Phone = saveDto.Phone,

                Session = Session
            };

            ResponseBo responseBo = personAddressBusiness.Save(personAddressBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Delete(PersonAddressDeleteDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonAddressDeleteBo deleteBo = new PersonAddressDeleteBo()
            {
                AddressId = deleteDto.AddressId,

                Session = Session
            };

            ResponseBo responseBo = personAddressBusiness.Delete(deleteBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }
    }
}