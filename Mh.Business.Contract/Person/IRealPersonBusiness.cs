using Mh.Business.Bo.Person.Real;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Person
{
    public  interface IRealPersonBusiness
    {
        ResponseBo<RealPersonBo> Get(BaseBo baseBo);

        ResponseBo ChangeLanguage(BaseBo baseBo);

        ResponseBo Update(RealPersonBo realPersonBo);
    }
}
