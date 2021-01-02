using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.IncludeExclude
{
    public class IncludeExcludeGetListCriteriaBo : BaseBo
    {
        public int CaseId { get; set; } // 0: get list by category, 1: get list by person product
        public bool IsInclude { get; set; }

        public int? ProductCategoryId { get; set; }
        public string Name { get; set; } // null. max length: 50
        public int PageOffSet { get; set; } // Works only in case of 0.

        public long? PersonProductId { get; set; }
    }
}