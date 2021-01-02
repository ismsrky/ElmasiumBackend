using Mh.Business.Bo.Help;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Help
{
    public interface IHelpBusiness
    {
        ResponseBo<List<HelpListBo>> GetList(HelpGetListCriteriaBo criteriaBo);
    }
}