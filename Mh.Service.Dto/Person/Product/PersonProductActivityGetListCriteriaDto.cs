using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Product
{
    public class PersonProductActivityGetListCriteriaDto
    {
        public long OwnerPersonId { get; set; }  // owner of the given products.
        public List<long> ProductIdList { get; set; } // cannot be null

        public List<Enums.ApprovalStats> ApprovalStatIdList { get; set; }

        public decimal? QuantityTotalMin { get; set; }
        public decimal? QuantityTotalMax { get; set; }

        public double? IssueDateStartNumber { get; set; }
        public double? IssueDateEndNumber { get; set; }

        public int PageOffSet { get; set; }
    }
}