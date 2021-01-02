using Mh.Business.Address;
using Mh.Business.Auth;
using Mh.Business.Contract.Address;
using Mh.Business.Contract.Auth;
using Mh.Business.Contract.Dictionary;
using Mh.Business.Contract.Person;
using Mh.Business.Contract.Product;
using Mh.Business.Dictionary;
using Mh.Business.Person;
using Mh.Business.Product;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using System.Reflection;
using System.Web.Http;

namespace Mh.Service.WebApi
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Filters.Add(new LoginAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}"
                //,defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);

            appBuilder.UseWebApi(config);

            config.EnableCors();
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            RegisterServices(kernel);

            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAddressBusiness>().To<AddressBusiness>().InThreadScope();

            kernel.Bind<IRealPersonBusiness>().To<RealPersonBusiness>().InThreadScope();
            kernel.Bind<IShopPersonBusiness>().To<ShopPersonBusiness>().InThreadScope();
            kernel.Bind<IPersonAddressBusiness>().To<PersonAddressBusiness>().InThreadScope();
            kernel.Bind<IPersonAccountBusiness>().To<PersonAccountBusiness>().InThreadScope();
            kernel.Bind<IProductBusiness>().To<ProductBusiness>().InThreadScope();
            kernel.Bind<IAuthBusiness>().To<AuthBusiness>().InThreadScope();

            kernel.Bind<IDictionaryBusiness>().To<DictionaryBusiness>().InThreadScope();
            kernel.Bind<ILanguageBusiness>().To<LanguageBusiness>().InThreadScope();
        }
    }
}