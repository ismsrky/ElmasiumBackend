using Mh.Business.Bo.IncludeExclude;
using Mh.Business.Bo.Person.Product;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.IncludeExclude
{
    public interface IIncludeExcludeBusiness
    {
        ResponseBo Save(IncludeExcludeSaveBo saveBo);
        ResponseBo SavePersonProduct(PersonProductIncludeExcludeBo saveBo);

        ResponseBo<List<IncludeExcludeBo>> GetList(IncludeExcludeGetListCriteriaBo criteriaBo);
    }
}