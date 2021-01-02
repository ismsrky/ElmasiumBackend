using System;
using System.Collections.Generic;
using System.Linq;

namespace Mh.Sessions
{
    public class SessionManager
    {
        public List<Session> SessionList { get; private set; }

        public void Init(List<Session> sessionList)
        {
            if (sessionList == null)
                SessionList = new List<Session>();
            else
                SessionList = sessionList;
        }

        public Session Login(SessionRealPerson personDto, DateTime loginTime, Guid tokenId, string clientIpAddress)
        {
            Session session = new Session();
            session.LoginTime = loginTime;
            session.TokenId = tokenId;

            session.RealPerson = personDto;

            session.ApiSessionId = personDto.ApiSessionId;
            session.ClientIpAddress = clientIpAddress;

            SessionList.Add(session);
            return session;
        }

        public void Logout(Guid tokenId)
        {
            Logout(Get(tokenId));
        }
        public void Logout(Session session)
        {
            session.LogoutTime = DateTime.Now;
        }

        public Session Get(Guid tokenId)
        {
            if (SessionList == null || SessionList.Count == 0) return null;

            Session t_session = SessionList.Where(x => x.TokenId == tokenId).FirstOrDefault();

            return t_session;
        }
    }
}