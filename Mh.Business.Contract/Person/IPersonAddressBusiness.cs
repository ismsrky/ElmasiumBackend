using Mh.Business.Bo.Person.Address;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Person
{
    public interface IPersonAddressBusiness
    {
        ResponseBo<PersonAddressBo> Get(PersonAddressGetCriteriaBo criteriaBo);
        ResponseBo<List<PersonAddressListBo>> GetList(PersonAddressGetListCriteriaBo criteriaBo);
        ResponseBo Save(PersonAddressBo saveBo);
        ResponseBo Delete(PersonAddressDeleteBo deleteBo);
    }
}