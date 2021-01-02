using Mh.Business.Bo.Option;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Bo.Person.Product
{
    public class PersonProductOptionBo : BaseBo
    {
        public long PersonProductId { get; set; }
        public List<OptionBo> OptionList { get; set; } // null. null means delete all option of that person product and it also mean there is no option of that person product.
    }
}