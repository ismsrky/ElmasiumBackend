using Mh.Business.Bo.Person;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Person
{
    public interface IPersonBusiness
    {
        ResponseBo<List<PersonListBo>> GetList(PersonGetListCriteriaBo criteriaBo);

        ResponseBo<PersonNotificationSummaryBo> GetNotificationSummary(BaseBo baseBo);

        ResponseBo<List<PersonNavMenuBo>> GetNavMenuList(BaseBo baseBo);
    }
}