using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Bo.Person.Address
{
    public class PersonAddressGetListCriteriaBo : BaseBo
    {
        public long OwnerPersonId { get; set; }

        public List<Enums.AddressTypes> AddressTypeIdList { get; set; }
        public Enums.EnumStats? StatId { get; set; }

        public List<long> AddressIdList { get; set; } // Other params are ignored if any value presents.
    }
}