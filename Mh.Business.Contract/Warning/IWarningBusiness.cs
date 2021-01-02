using Mh.Business.Bo.Sys;
using Mh.Business.Bo.Warning;

namespace Mh.Business.Contract.Warning
{
    public interface IWarningBusiness
    {
        ResponseBo Save(WarningBo saveBo);

        ResponseBo SaveCheck(WarningCheckBo saveCheckBo);
    }
}