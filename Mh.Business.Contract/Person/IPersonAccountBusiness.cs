using Mh.Business.Bo.Person.Account;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Person
{
    public interface IPersonAccountBusiness
    {
        ResponseBo<PersonAccountBo> Get(PersonAccountGetCriteriaBo criteriaBo);
        ResponseBo<List<PersonAccountListBo>> GetList(PersonAccountGetListCriteriaBo criteriaBo);

        ResponseBo Save(PersonAccountBo personAccountBo);

        ResponseBo Delete(PersonAccountDeleteBo deleteBo);

        ResponseBo<List<PersonAccountActivityListBo>> GetActivityList(PersonAccountActivityGetListCriteriaBo criteriaBo);

        ResponseBo GetFastRetail(PersonAccountGetFastRetailCriteriaBo criteriaBo);
    }
}