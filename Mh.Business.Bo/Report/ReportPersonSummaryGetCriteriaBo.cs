using Mh.Business.Bo.Sys;
using System;

namespace Mh.Business.Bo.Report
{
    public class ReportPersonSummaryGetCriteriaBo : BaseBo
    {
        // Following dates cannot be null.
        public DateTime IssueDateStart { get; set; }
        public DateTime IssueDateEnd { get; set; }

        public Enums.Currencies CurrencyId { get; set; }
    }
}