using Mh.Business.Bo.Person.Alone;
using Mh.Business.Bo.Sys;

namespace Mh.Business.Contract.Person
{
    public interface IAlonePersonBusiness
    {
        ResponseBo<AlonePersonBo> Get(AlonePersonGetCriteriaBo criteriaBo);
        ResponseBo Save(AlonePersonBo saveBo);
    }
}
