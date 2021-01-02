namespace Mh.Service.Dto.Report
{
    public class ReportPersonSummaryGetCriteriaDto
    {
        // Following dates cannot be null.
        public double IssueDateStartNumber { get; set; }
        public double IssueDateEndNumber { get; set; }

        public Enums.Currencies CurrencyId { get; set; }
    }
}