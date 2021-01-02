namespace Mh.Service.Dto.Help
{
    public class HelpListDto
    {
        public double CreateDateNumber { get; set; } // not null
        public double? UpdateDateNumber { get; set; } // null

        public string VideoUrl { get; set; } // null
        public string ImageUrl { get; set; } // null
        public bool IsTextHtml { get; set; } // not null
        public string Caption { get; set; } // not null
        public string Text { get; set; } // null

        public int Order { get; set; } // not null
    }
}