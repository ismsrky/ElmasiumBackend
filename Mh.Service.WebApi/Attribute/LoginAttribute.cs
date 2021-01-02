using System;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Mh.Service.WebApi.Attribute
{
    public class LoginAttribute : AuthorizeAttribute
    {
        public LoginAttribute()
        {

        }

        public bool CheckAuthorized(HttpActionContext actionContext, out Sessions.Session session)
        {
            session = null;
            try
            {
                //tokenId gönderilmemiş ise geri çevrilir. Güle güle.
                if (actionContext == null)
                {
                    return false;
                }

                Guid? tokenId = actionContext.Request.GetTokenId();

                if (tokenId == null) return false;

                session = Stc.SessionManager.Get(tokenId.Value);
                //Verilen tokenId'ye ait oturum bulunamadı.
                if ((session == null || session.LogoutTime != null))
                {
                    return false;
                }


                //string ipAddress = actionContext.Request.GetClientIpAddress();
                ////Ip adresleri birbirlerini tutmuyor.
                //if (ipAddress.IsNull() || ipAddress != session.IpAdress)
                //{
                //    return false;
                //}



                //if (actionContext.Request.Headers.Authorization.Scheme == "58E6E25C-39FD-4A31-9080-6FD5AB682A80")
                //{
                //    return true;
                //}


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            Sessions.Session session = null;
            return CheckAuthorized(actionContext, out session);
        }
    }
}