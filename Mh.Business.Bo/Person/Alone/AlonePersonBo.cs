using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Alone
{
    public class AlonePersonBo : BaseBo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public Enums.PersonTypes PersonTypeId { get; set; }

        public Enums.EnumStats StatId { get; set; }
        public Enums.Currencies DefaultCurrencyId { get; set; }

        public string Phone { get; set; }
        public string Notes { get; set; }

        public long ParentRelationPersonId { get; set; }
        public Enums.RelationTypes ChildRelationTypeId { get; set; }

        public long? PersonAddressId { get; set; }  // null means no address record.
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }

        public int? AddressCountryId { get; set; }
        public int? AddressStateId { get; set; }
        public int? AddressCityId { get; set; }
        public int? AddressDistrictId { get; set; }
        public string AddressNotes { get; set; }
    }
}