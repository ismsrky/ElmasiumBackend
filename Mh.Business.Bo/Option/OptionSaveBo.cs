using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Option
{
    public class OptionSaveBo : BaseBo
    {
        public int ProductCategoryId { get; set; }

        public string Name { get; set; }
        public string OptionNameListStr { get; set; }
    }
}