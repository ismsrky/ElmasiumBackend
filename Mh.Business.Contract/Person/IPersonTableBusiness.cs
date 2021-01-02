using Mh.Business.Bo.Person.Table;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Person
{
    public interface IPersonTableBusiness
    {
        ResponseBo Save(PersonTableBo saveBo);
        ResponseBo<PersonTableBo> Get(PersonTableGetCriteriaBo criteriaBo);
        ResponseBo Delete(PersonTableDeleteBo deleteBo);
        ResponseBo<List<PersonTableListBo>> GetList(PersonTableGetListCriteriaBo criteriaBo);


        ResponseBo SaveGroup(PersonTableGroupBo saveBo);
        ResponseBo<PersonTableGroupBo> GetGroup(PersonTableGroupGetCriteriaBo criteriaBo);
        ResponseBo DeleteGroup(PersonTableGroupDeleteBo deleteBo);
        ResponseBo<List<PersonTableGroupListBo>> GetGroupList(PersonTableGroupGetListCriteriaBo criteriaBo);
    }
}