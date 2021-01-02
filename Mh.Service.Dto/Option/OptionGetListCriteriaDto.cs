namespace Mh.Service.Dto.Option
{
    public class OptionGetListCriteriaDto
    {
        public int CaseId { get; set; } // 0: get list by category, 1: get list by person product, 2: get list by group.

        public int? ProductCategoryId { get; set; }
        public long? PersonProductId { get; set; }
        public int? OptionGroupId { get; set; }
    }
}