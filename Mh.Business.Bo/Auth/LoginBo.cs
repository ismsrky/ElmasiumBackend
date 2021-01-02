using System;

namespace Mh.Business.Bo.Auth
{
    public class LoginBo
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Enums.Languages LanguageId { get; set; }
        public DateTime LoginTime { get; set; }
        public Guid TokenId { get; set; }
        public string ClientIpAddress { get; set; }

        public long? AnonymousApiSessionId { get; set; }
    }
}