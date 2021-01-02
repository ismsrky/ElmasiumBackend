using System;

namespace Mh.Business.Bo.Help
{
    public class HelpListBo
    {
        public DateTime CreateDate { get; set; } // not null
        public DateTime? UpdateDate { get; set; } // null

        public string VideoUrl { get; set; } // null
        public string ImageUrl { get; set; } // null
        public bool IsTextHtml { get; set; } // not null
        public string Caption { get; set; } // not null
        public string Text { get; set; } // null

        public int Order { get; set; } // not null
    }
}