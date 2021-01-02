using System;

namespace Mh.Sessions
{
    public class Session
    {
        public Guid TokenId { get; internal set; }
        public long ApiSessionId { get; internal set; }
        public string ClientIpAddress { get; internal set; }

        //public DateTime StartTime { get; internal set; }

        public DateTime LoginTime { get; internal set; }
        public DateTime? LogoutTime { get; internal set; }

        public SessionRealPerson RealPerson { get; set; }

        public SessionMyPerson MyPerson { get; set; }

        public string RealPersonJson { get; set; }
        public string MyPersonJson { get; set; }

        public Session Copy()
        {
            Session session = new Session();

            session.TokenId = TokenId;
            session.ApiSessionId = ApiSessionId;
            session.ClientIpAddress = ClientIpAddress;

            session.LoginTime = LoginTime;
            session.LogoutTime = LogoutTime;

            session.RealPerson = RealPerson.Copy();
            session.MyPerson = MyPerson.Copy();

            return session;
        }
    }
}