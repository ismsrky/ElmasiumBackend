using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Sys
{
    public interface ISysBusiness
    {
        ResponseBo<List<SysMailBo>> GetMailList();

        ResponseBo<SysVersionBo> GetLatestVersion(SysVersionGetLatestCriteriaBo criteriaBo);
    }
}