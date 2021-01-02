using System;

namespace Mh.Db.Tables
{
    public class Person
    {
        public int Id { get; set; } // int, not null
        public string Name { get; set; } // nvarchar(50), null
        public string Surname { get; set; } // nvarchar(50), null
        public string CompanyName { get; set; } // nvarchar(500), null
        public string CompanyBrand { get; set; } // nvarchar(50), null
        public int StatId { get; set; } // int, not null
        public int PersonTypeId { get; set; } // int, not null
        public string Username { get; set; } // nvarchar(250), null
        public string Password { get; set; } // nvarchar(250), null
        public string Email { get; set; } // nvarchar(250), null
        public int? LanguageId { get; set; } //int, null
        public int? SectorId { get; set; }
        public int? CreatePersonId { get; set; } // int, null
        public DateTime CreateDate { get; set; } // datetime, not null
        public int? UpdatePersonId { get; set; } // int, null
        public DateTime? UpdateDate { get; set; } // datetime, null
    }
}