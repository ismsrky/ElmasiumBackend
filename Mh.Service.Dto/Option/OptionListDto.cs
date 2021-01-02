using System.Collections.Generic;

namespace Mh.Service.Dto.Option
{
    public class OptionListDto
    {
        public int Id { get; set; } // Gruoup Id
        public string Name { get; set; } // Group name
        public string UrlName { get; set; } // Group url name

        public decimal PriceGap { get; set; } // can be 0, < 0 and > 0

        public List<OptionListDto> OptionList { get; set; }
    }
}