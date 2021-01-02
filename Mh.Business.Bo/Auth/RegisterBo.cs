using System;

namespace Mh.Business.Bo.Auth
{
    public class RegisterBo
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Enums.Languages LanguageId { get; set; }

        public Enums.Genders? GenderId { get; set; }
        public DateTime? Birthdate { get; set; }

        public bool HaveShopToo { get; set; }
        public string ShopShortName { get; set; }
        public Enums.ShopTypes? ShopTypeId { get; set; }
    }
}