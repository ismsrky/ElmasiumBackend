using System.Web.Http.Controllers;

namespace Mh.Service.WebApi.Attribute
{
    public class AdminAttribute: LoginAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            Sessions.Session session = null;
            bool result = base.CheckAuthorized(actionContext, out session);



            return result & session != null & session.RealPerson != null & Stc.AdminRealIdList.Contains(session.RealPerson.Id);
        }
    }
}