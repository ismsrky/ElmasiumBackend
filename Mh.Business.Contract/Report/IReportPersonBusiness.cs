using Mh.Business.Bo.Report;
using Mh.Business.Bo.Sys;

namespace Mh.Business.Contract.Report
{
    public interface IReportPersonBusiness
    {
        ResponseBo<ReportPersonSummaryBo> GetSummary(ReportPersonSummaryGetCriteriaBo criteriaBo);
    }
}