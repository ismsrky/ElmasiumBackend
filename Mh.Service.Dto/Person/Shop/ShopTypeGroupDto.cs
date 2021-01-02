using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Shop
{
    public class ShopTypeGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ShopTypeDto> TypeList { get; set; }
    }
}