using System.Collections.Generic;

namespace Mh.Service.Dto.EnumsOp
{
    public class ShopTypeGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int GroupOrder { get; set; }

        public List<ShopTypeDto> TypeList { get; set; }
    }
}