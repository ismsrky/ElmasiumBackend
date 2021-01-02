using System;

namespace Mh.Service.Dto.Auth
{
    public class LoginReturnDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Enums.Currencies DefaultCurrencyId { get; set; }
        public Enums.Genders GenderId { get; set; }

        public Guid TokenId { get; set; }
    }
}