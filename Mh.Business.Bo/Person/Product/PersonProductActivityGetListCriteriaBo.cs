using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;

namespace Mh.Business.Bo.Person.Product
{
    public class PersonProductActivityGetListCriteriaBo : BaseBo
    {
        public long OwnerPersonId { get; set; }  // owner of the given products
        public List<long> ProductIdList { get; set; } // cannot be null

        public List<Enums.ApprovalStats> ApprovalStatIdList { get; set; }

        public decimal? QuantityTotalMin { get; set; }
        public decimal? QuantityTotalMax { get; set; }

        public DateTime? IssueDateStart { get; set; }
        public DateTime? IssueDateEnd { get; set; }

        public int PageOffSet { get; set; }
    }
}