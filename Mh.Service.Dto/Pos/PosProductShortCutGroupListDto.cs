using System.Collections.Generic;

namespace Mh.Service.Dto.Pos
{
    public class PosProductShortCutGroupListDto
    {
        public long Id { get; set; } // Group Id
        public string Name { get; set; } // Group name

        public List<PosProductShortCutListDto> ProductList { get; set; }
    }
}