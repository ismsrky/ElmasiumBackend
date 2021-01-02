using System;
using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Account
{
    public class PersonAccountActivityGetListCriteriaDto
    {
        public long OwnerPersonId { get; set; }  // owner of the given accounts
        public List<long> AccountIdList { get; set; } // cannot be null

        public List<Enums.ApprovalStats> ApprovalStatIdList { get; set; }

        public decimal? GrandTotalMin { get; set; }
        public decimal? GrandTotalMax { get; set; }

        public Enums.Currencies CurrencyId { get; set; }

        public double? IssueDateStartNumber { get; set; }
        public double? IssueDateEndNumber { get; set; }


        public int PageOffSet { get; set; }
    }
}