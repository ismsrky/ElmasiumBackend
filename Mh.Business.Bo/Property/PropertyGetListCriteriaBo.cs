using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Property
{
    public class PropertyGetListCriteriaBo : BaseBo
    {
        public int CaseId { get; set; } // 0: get list by category, 1: get list by person product, 2: get list by group.

        public int? ProductCategoryId { get; set; }
        public long? PersonProductId { get; set; }
        public int? PropertyGroupId { get; set; }
    }
}