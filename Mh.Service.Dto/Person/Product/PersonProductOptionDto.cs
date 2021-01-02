using Mh.Service.Dto.Option;
using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Product
{
    public class PersonProductOptionDto
    {
        public long PersonProductId { get; set; }
        public List<OptionDto> OptionList { get; set; } // null. null means delete all option of that person product and it also mean there is no option of that person product.
    }
}