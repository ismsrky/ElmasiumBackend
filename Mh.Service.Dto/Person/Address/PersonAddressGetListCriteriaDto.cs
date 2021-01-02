using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Address
{
    public class PersonAddressGetListCriteriaDto
    {
        public long OwnerPersonId { get; set; }

        public List<Enums.AddressTypes> AddressTypeIdList { get; set; }
        public Enums.EnumStats? StatId { get; set; }

        public List<long> AddressIdList { get; set; } // Other params are ignored if any value presents.
    }
}