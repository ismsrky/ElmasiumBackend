using System;

namespace Mh.Db.Tables
{
    public class PersonAddress
    {
        public int Id { get; set; } // int, not null
        public int AddressTypeId { get; set; } // int, not null
        public int PersonId { get; set; } // int, not null
        public int StatId { get; set; } // int, not null
        public string Name { get; set; } // nvarchar(50), not null
        public int? CityId { get; set; } // int, null
        public int? DistrictId { get; set; } // int, null
        public int? LocalityId { get; set; } // int, null
        public int? QuarterId { get; set; } // int, null
        public string Notes { get; set; } // varchar(250), null
        public string ZipCode { get; set; } // nvarchar(50), null
        public string Phone { get; set; } // nvarchar(50), null
        public string Fax { get; set; } // nvarchar(50), null
        public int CreatePersonId { get; set; } // int, not null
        public DateTime CreateDate { get; set; } // datetime, not null
        public int? UpdatePersonId { get; set; } // int, null
        public DateTime? UpdateDate { get; set; } // datetime, null
    }
}
