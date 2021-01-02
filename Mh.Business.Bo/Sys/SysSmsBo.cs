namespace Mh.Business.Bo.Sys
{
    public class SysSmsBo
    {
        public int Id { get; set; }

        public string UrlAddress { get; set; } // not null
        public string OtpUrlAddress { get; set; } // null

        public string Username { get; set; } // not null
        public string Password { get; set; } // not null

        public string CompanyName { get; set; } // null
    }
}