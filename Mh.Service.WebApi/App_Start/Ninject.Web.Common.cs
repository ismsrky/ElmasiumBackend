[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Mh.Service.WebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Mh.Service.WebApi.App_Start.NinjectWebCommon), "Stop")]

namespace Mh.Service.WebApi.App_Start
{
    using Mh.Business.Address;
    using Mh.Business.Approval;
    using Mh.Business.Auth;
    using Mh.Business.Basket;
    using Mh.Business.Comment;
    using Mh.Business.Contract.Address;
    using Mh.Business.Contract.Approval;
    using Mh.Business.Contract.Auth;
    using Mh.Business.Contract.Basket;
    using Mh.Business.Contract.Comment;
    using Mh.Business.Contract.Dashboard;
    using Mh.Business.Contract.Dictionary;
    using Mh.Business.Contract.Fiche;
    using Mh.Business.Contract.Help;
    using Mh.Business.Contract.Image;
    using Mh.Business.Contract.IncludeExclude;
    using Mh.Business.Contract.Log;
    using Mh.Business.Contract.Notification;
    using Mh.Business.Contract.Option;
    using Mh.Business.Contract.Order;
    using Mh.Business.Contract.Person;
    using Mh.Business.Contract.Pos;
    using Mh.Business.Contract.Product;
    using Mh.Business.Contract.Property;
    using Mh.Business.Contract.Report;
    using Mh.Business.Contract.Sys;
    using Mh.Business.Contract.Warning;
    using Mh.Business.Dashboard;
    using Mh.Business.Dictionary;
    using Mh.Business.Fiche;
    using Mh.Business.Help;
    using Mh.Business.Image;
    using Mh.Business.IncludeExclude;
    using Mh.Business.Log;
    using Mh.Business.Notification;
    using Mh.Business.Option;
    using Mh.Business.Order;
    using Mh.Business.Person;
    using Mh.Business.Pos;
    using Mh.Business.Product;
    using Mh.Business.Property;
    using Mh.Business.Report;
    using Mh.Business.Sys;
    using Mh.Business.Warning;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using System;
    using System.Web;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IPersonBusiness>().To<PersonBusiness>().InThreadScope();
            kernel.Bind<IRealPersonBusiness>().To<RealPersonBusiness>().InThreadScope();
            kernel.Bind<IAlonePersonBusiness>().To<AlonePersonBusiness>().InThreadScope();
            kernel.Bind<IShopPersonBusiness>().To<ShopPersonBusiness>().InThreadScope();
            kernel.Bind<IPersonAddressBusiness>().To<PersonAddressBusiness>().InThreadScope();
            kernel.Bind<IPersonAccountBusiness>().To<PersonAccountBusiness>().InThreadScope();
            kernel.Bind<IPersonProductBusiness>().To<PersonProductBusiness>().InThreadScope();
            kernel.Bind<IPersonRelationBusiness>().To<PersonRelationBusiness>().InThreadScope();
            kernel.Bind<IPersonRelationRuleBusiness>().To<PersonRelationRuleBusiness>().InThreadScope();
            kernel.Bind<IPersonTableBusiness>().To<PersonTableBusiness>().InThreadScope();
            kernel.Bind<IPersonVerifyPhoneBusiness>().To<PersonVerifyPhoneBusiness>().InThreadScope();

            kernel.Bind<IProductBusiness>().To<ProductBusiness>().InThreadScope();
            kernel.Bind<IProductCodeBusiness>().To<ProductCodeBusiness>().InThreadScope();
            kernel.Bind<IProductCategoryBusiness>().To<ProductCategoryBusiness>().InThreadScope();
            kernel.Bind<IProductFilterBusiness>().To<ProductFilterBusiness>().InThreadScope();

            kernel.Bind<IAddressBusiness>().To<AddressBusiness>().InThreadScope();

            kernel.Bind<IAuthBusiness>().To<AuthBusiness>().InThreadScope();

            kernel.Bind<IFicheBusiness>().To<FicheBusiness>().InThreadScope();
            kernel.Bind<IFicheProductBusiness>().To<FicheProductBusiness>().InThreadScope();
            kernel.Bind<IFicheMoneyBusiness>().To<FicheMoneyBusiness>().InThreadScope();
            kernel.Bind<IFicheRelationBusiness>().To<FicheRelationBusiness>().InThreadScope();

            kernel.Bind<IPosBusiness>().To<PosBusiness>().InThreadScope();

            kernel.Bind<IApprovalFicheBusiness>().To<ApprovalFicheBusiness>().InThreadScope();
            kernel.Bind<IApprovalRelationBusiness>().To<ApprovalRelationBusiness>().InThreadScope();

            kernel.Bind<INotificationBusiness>().To<NotificationBusiness>().InThreadScope();
            kernel.Bind<INotificationPreferenceBusiness>().To<NotificationPreferenceBusiness>().InThreadScope();

            kernel.Bind<IReportPersonBusiness>().To<ReportPersonBusiness>().InThreadScope();

            kernel.Bind<IWarningBusiness>().To<WarningBusiness>().InThreadScope();

            kernel.Bind<IHelpBusiness>().To<HelpBusiness>().InThreadScope();
            kernel.Bind<ISysBusiness>().To<SysBusiness>().InThreadScope();

            kernel.Bind<IImageBusiness>().To<ImageBusiness>().InThreadScope();

            kernel.Bind<IDictionaryBusiness>().To<DictionaryBusiness>().InThreadScope();
            kernel.Bind<ILanguageBusiness>().To<LanguageBusiness>().InThreadScope();
            kernel.Bind<ILogExceptionBusiness>().To<LogExceptionBusiness>().InThreadScope();

            kernel.Bind<ICommentBusiness>().To<CommentBusiness>().InThreadScope();

            kernel.Bind<IPropertyBusiness>().To<PropertyBusiness>().InThreadScope();
            kernel.Bind<IOptionBusiness>().To<OptionBusiness>().InThreadScope();
            kernel.Bind<IIncludeExcludeBusiness>().To<IncludeExcludeBusiness>().InThreadScope();

            kernel.Bind<IBasketBusiness>().To<BasketBusiness>().InThreadScope();
            kernel.Bind<IBasketProductBusiness>().To<BasketProductBusiness>().InThreadScope();

            kernel.Bind<IOrderBusiness>().To<OrderBusiness>().InThreadScope();
            kernel.Bind<IOrderStatHistoryBusiness>().To<OrderStatHistoryBusiness>().InThreadScope();

            kernel.Bind<IDashboardSliderBusiness>().To<DashboardSliderBusiness>().InThreadScope();
        }        
    }
}