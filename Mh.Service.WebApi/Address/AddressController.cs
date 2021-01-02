using Mh.Business.Bo.Address;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Address;
using Mh.Service.Dto.Address;
using Mh.Service.Dto.Sys;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mh.Service.WebApi.Address
{
    public class AddressController : BaseController
    {
        readonly IAddressBusiness addressBusiness;
        public AddressController(IAddressBusiness _addressBusiness)
        {
            addressBusiness = _addressBusiness;
        }

        [HttpPost]
        public ResponseDto<List<AddressCountryDto>> GetCountryList()
        {
            ResponseBo<List<AddressCountryBo>> responseBo = addressBusiness.GetCountryList(base.ToBaseBo());

            ResponseDto<List<AddressCountryDto>> responseDto = responseBo.ToResponseDto<List<AddressCountryDto>, List<AddressCountryBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<AddressCountryDto>();
                foreach (AddressCountryBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new AddressCountryDto()
                    {
                        Id = itemBo.Id,
                        Name = itemBo.Name,
                        HasStates = itemBo.HasStates
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<AddressStateDto>> GetStateList(AddressGetStateListCriteriaDto criteriaDto)
        {
            AddressGetStateListCriteriaBo criteriaBo = new AddressGetStateListCriteriaBo()
            {
                CountryId = criteriaDto.CountryId,

                Session = Session
            };

            ResponseBo<List<AddressStateBo>> responseBo = addressBusiness.GetStateList(criteriaBo);

            ResponseDto<List<AddressStateDto>> responseDto = responseBo.ToResponseDto<List<AddressStateDto>, List<AddressStateBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<AddressStateDto>();
                foreach (AddressStateBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new AddressStateDto()
                    {
                        Id = itemBo.Id,
                        CountryId = itemBo.CountryId,
                        Name = itemBo.Name
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<AddressCityDto>> GetCityList(AddressGetCityListCriteriaDto criteriaDto)
        {
            AddressGetCityListCriteriaBo criteriaBo = new AddressGetCityListCriteriaBo()
            {
                CountryId = criteriaDto.CountryId,
                StateId = criteriaDto.StateId,

                Session = Session
            };

            ResponseBo<List<AddressCityBo>> responseBo = addressBusiness.GetCityList(criteriaBo);

            ResponseDto<List<AddressCityDto>> responseDto = responseBo.ToResponseDto<List<AddressCityDto>, List<AddressCityBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<AddressCityDto>();
                foreach (AddressCityBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new AddressCityDto()
                    {
                        Id = itemBo.Id,
                        Name = itemBo.Name,
                        Plate = itemBo.Plate,

                        StatedId = itemBo.StatedId,
                        CountryId = itemBo.CountryId
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<AddressDistrictDto>> GetDistrictList(AddressGetDistrictListCriteriaDto criteriaDto)
        {
            AddressGetDistrictListCriteriaBo criteriaBo = new AddressGetDistrictListCriteriaBo()
            {
                CityId = criteriaDto.CityId,

                Session = Session
            };

            ResponseBo<List<AddressDistrictBo>> responseBo = addressBusiness.GetDistrictList(criteriaBo);

            ResponseDto<List<AddressDistrictDto>> responseDto = responseBo.ToResponseDto<List<AddressDistrictDto>, List<AddressDistrictBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<AddressDistrictDto>();
                foreach (AddressDistrictBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new AddressDistrictDto()
                    {
                        Id = itemBo.Id,
                        CityId = itemBo.CityId,
                        Name = itemBo.Name
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<AddressLocalityDto>> GetLocalityList(AddressGetLocalityListCriteriaDto criteriaDto)
        {
            AddressGetLocalityListCriteriaBo criteriaBo = new AddressGetLocalityListCriteriaBo()
            {
                DistrictId = criteriaDto.DistrictId,

                Session = Session
            };

            ResponseBo<List<AddressLocalityBo>> responseBo = addressBusiness.GetLocalityList(criteriaBo);

            ResponseDto<List<AddressLocalityDto>> responseDto = responseBo.ToResponseDto<List<AddressLocalityDto>, List<AddressLocalityBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<AddressLocalityDto>();
                foreach (AddressLocalityBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new AddressLocalityDto()
                    {
                        Id = itemBo.Id,
                        DistrictId = itemBo.DistrictId,
                        Name = itemBo.Name
                    });
                }
            }

            return responseDto;
        }
    }
}