using System;

namespace Mh.Service.Dto.Auth
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public Enums.Languages LanguageId { get; set; }

        public Enums.Genders? GenderId { get; set; }
        public double? BirthdateNumber { get; set; }
        public string Phone { get; set; }

        public bool HaveShopToo { get; set; }
        public string ShopShortName { get; set; }
        public Enums.ShopTypes? ShopTypeId { get; set; }
    }
}