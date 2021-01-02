using System.Collections.Generic;

namespace Mh.Service.Dto.EnumsOp
{
    public class FicheContentGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<FicheContentDto> ContentList { get; set; }
    }
}