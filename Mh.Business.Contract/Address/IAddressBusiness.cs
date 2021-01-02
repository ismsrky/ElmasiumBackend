using Mh.Business.Bo.Address;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Address
{
    public interface IAddressBusiness
    {
        ResponseBo<List<AddressCountryBo>> GetCountryList(BaseBo baseBo);

        ResponseBo<List<AddressStateBo>> GetStateList(AddressGetStateListCriteriaBo criteriaBo);

        ResponseBo<List<AddressCityBo>> GetCityList(AddressGetCityListCriteriaBo criteriaBo);

        ResponseBo<List<AddressDistrictBo>> GetDistrictList(AddressGetDistrictListCriteriaBo criteriaBo);

        ResponseBo<List<AddressLocalityBo>> GetLocalityList(AddressGetLocalityListCriteriaBo criteriaBo);
    }
}