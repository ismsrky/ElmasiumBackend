using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Bo.Property
{
    public class PropertySaveBo : BaseBo
    {
        public int ProductCategoryId { get; set; }

        public string Name { get; set; }
        public string PropertyNameListStr { get; set; }
    }
}