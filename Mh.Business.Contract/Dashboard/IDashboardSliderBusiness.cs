using Mh.Business.Bo.Dashboard.Slider;
using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Contract.Dashboard
{
    public interface IDashboardSliderBusiness
    {
        ResponseBo<List<DashboardSliderGroupListBo>> GetGroupList(BaseBo baseBo);
        ResponseBo<List<DashboardSliderListBo>> GetList(DashboardSliderGetListCriteriaBo criteriaBo);
    }
}