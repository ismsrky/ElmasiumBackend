using Mh.Business.Bo.Dictionary;
using Mh.Business.Bo.Sys;
using Mh.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mh.Service.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {       
        protected void Application_Start()
        {
            //GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(NinjectWebCommon.CreateKernel());

            Encryption.Key = new byte[] { 28, 14, 79, 24, 168, 2, 142, 10, 198, 6,
                89, 93, 115, 12, 253, 71, 62, 221, 55, 121, 38, 174, 6, 51, 168, 54, 2, 26, 228, 113, 32, 109 };
            Encryption.Vector = new byte[] { 195, 74, 41, 17, 219, 10, 64, 41, 81, 39, 209, 165, 4, 86, 236, 88 };

            Stc.ReadConfigs(); //Reads app.config file
            Business.Stc.ConnStr = Stc.ConnStr;


            Business.Auth.AuthBusiness authBusiness = new Business.Auth.AuthBusiness();
            Stc.SessionManager = new Sessions.SessionManager();
            Stc.SessionManager.Init(authBusiness.GetSessionList());

            // Dictionaries
            Business.Dictionary.DictionaryBusiness dictionaryBusiness = new Business.Dictionary.DictionaryBusiness();
            ResponseBo<List<DictionaryBo>> responseDic = dictionaryBusiness.GetList();
            Business.Stc.DicItemList = responseDic.Bo.Select(x => x.ToDicItem()).ToList();

            // Enums
            Business.EnumsOp.EnumsOpBusiness enumsOpBusiness = new Business.EnumsOp.EnumsOpBusiness();
            Business.Stc.EnumShopTypeList = enumsOpBusiness.GetShopTypeList().Bo;
            Business.Stc.EnumCurrencyList = enumsOpBusiness.GetCurrencyList().Bo;
            Business.Stc.EnumFicheContentList = enumsOpBusiness.GetFicheContentList().Bo;

            try
            {
                Ws.wsConnManager.InitAndStart();
            }
            catch (Exception ex)
            {
            }

            System.Timers.Timer tm = new System.Timers.Timer();
            tm.Interval = 5 * 60 * 1000;
            tm.Elapsed += Tm_Elapsed;
            tm.Start();

            System.Timers.Timer tm_dic = new System.Timers.Timer();
            tm_dic.Interval = 10 * 60 * 1000;
            tm_dic.Elapsed += TmDic_Elapsed;
            tm_dic.Start();

            System.Timers.Timer tm_session = new System.Timers.Timer();
            tm_session.Interval = 12 * 60 * 1000;
            tm_session.Elapsed += Tm_session_Elapsed;
            tm_session.Start();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_End()
        {
            SaveSessionState();
        }

        void SaveSessionState()
        {
#if !DEBUG
            Business.Auth.AuthBusiness authBusiness = new Business.Auth.AuthBusiness();
            authBusiness.SaveSessionState(Stc.SessionManager.SessionList);
#endif
        }

        private void Tm_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {                
                Ws.wsConnManager.InitAndStart();
            }
            catch (Exception ex)
            {
            }
        }

        private void TmDic_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Business.Dictionary.DictionaryBusiness dictionaryBusiness = new Business.Dictionary.DictionaryBusiness();
                ResponseBo<List<DictionaryBo>> responseDic = dictionaryBusiness.GetList();
                Business.Stc.DicItemList = responseDic.Bo.Select(x => x.ToDicItem()).ToList();
            }
            catch (Exception ex)
            {
            }
        }

        private void Tm_session_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                SaveSessionState();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
