using Mh.Business.Bo.Sys;
using System;

namespace Mh.Business.Bo.Person.Real
{
    public class RealPersonBo : BaseBo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public Enums.EnumStats StatId { get; set; }
        public Enums.Currencies DefaultCurrencyId { get; set; }

        public string Phone { get; set; }
        public Enums.Genders? GenderId { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}