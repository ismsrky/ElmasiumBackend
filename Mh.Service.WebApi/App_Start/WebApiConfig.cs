using Mh.Service.WebApi.Attribute;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Mh.Service.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Important security warning. Enable cross-origin only when in debug.
#if DEBUG
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
#endif

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new LoginAttribute());

            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "{controller}/{action}"
             //,defaults: new { id = RouteParameter.Optional }
             );
        }
    }
}