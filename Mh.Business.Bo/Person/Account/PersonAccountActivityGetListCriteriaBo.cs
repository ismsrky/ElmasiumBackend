using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;

namespace Mh.Business.Bo.Person.Account
{
    public class PersonAccountActivityGetListCriteriaBo : BaseBo
    {
        public long OwnerPersonId { get; set; }  // owner of the given accounts
        public List<long> AccountIdList { get; set; } // cannot be null

        public List<Enums.ApprovalStats> ApprovalStatIdList { get; set; }

        public decimal? GrandTotalMin { get; set; }
        public decimal? GrandTotalMax { get; set; }

        public Enums.Currencies CurrencyId { get; set; }

        public DateTime? IssueDateStart { get; set; }
        public DateTime? IssueDateEnd { get; set; }


        public int PageOffSet { get; set; }
    }
}