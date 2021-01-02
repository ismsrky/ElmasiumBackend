using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.IncludeExclude
{
    public class IncludeExcludeSaveBo : BaseBo
    {
        public int ProductCategoryId { get; set; }
        public bool IsInclude { get; set; }

        // One of follow values must be given. Only admins can give a list.
        public string IncludeExcludeNameListStr { get; set; } // null or not null
        public string IncludeExcludeName { get; set; } // null or not null. max length: 50
    }
}