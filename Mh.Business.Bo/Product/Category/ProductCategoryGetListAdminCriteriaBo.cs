﻿using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Bo.Product.Category
{
    public class ProductCategoryGetListAdminCriteriaBo : BaseBo
    {
        public int? ParentId { get; set; }
    }
}