using Mh.Business.Bo.Dictionary;
using Mh.Business.Bo.Sys;
using Mh.Utils;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace Mh.Service.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Encryption.Key = new byte[] { 28, 14, 79, 24, 168, 2, 142, 10, 198, 6,
                89, 93, 115, 12, 253, 71, 62, 221, 55, 121, 38, 174, 6, 51, 168, 54, 2, 26, 228, 113, 32, 109 };
            Encryption.Vector = new byte[] { 195, 74, 41, 17, 219, 10, 64, 41, 81, 39, 209, 165, 4, 86, 236, 88 };


            Stc.ReadConfigs(); //Reads app.config file

            Stc.SessionManager = new Sessions.SessionManager();
            Stc.SessionManager.Init();

            Business.Stc.ConnStr = Stc.ConnStr;

            // Dictionaries
            Business.Dictionary.DictionaryBusiness dictionaryBusiness = new Business.Dictionary.DictionaryBusiness();
            ResponseBo<List<DictionaryBo>> responseDic = dictionaryBusiness.GetList();
            Business.Stc.DicItemList = responseDic.Bo.Select(x => x.ToDicItem()).ToList();

            // Addresses
            Business.Address.AddressBusiness addressBusiness = new Business.Address.AddressBusiness();
            Business.Stc.AddressCountryList = addressBusiness.GetCountryList().Bo;
            Business.Stc.AddressStateList = addressBusiness.GetStateList().Bo;
            Business.Stc.AddressCityList = addressBusiness.GetCityList().Bo;
            Business.Stc.AddressDistrictList = addressBusiness.GetDistrictList().Bo;

            // Enums
            Business.EnumsOp.EnumsOpBusiness enumsOpBusiness = new Business.EnumsOp.EnumsOpBusiness();
            Business.Stc.EnumShopTypeList = enumsOpBusiness.GetShopTypeList().Bo;
            Business.Stc.EnumCurrencyList = enumsOpBusiness.GetCurrencyList().Bo;
            Business.Stc.EnumFicheContentList = enumsOpBusiness.GetFicheContentList().Bo;

            // Others
            Business.Product.ProductCategoryBusiness productCategoryBusiness = new Business.Product.ProductCategoryBusiness();
            Business.Stc.ProductCategoryList = productCategoryBusiness.GetList().Bo;

            //Business.Person.RealPersonBusiness realPersonBusiness = new Business.Person.RealPersonBusiness();
            //realPersonBusiness.GetRealPerson(1);

            //Timer tm = new Timer(Callback, null, 0, Timeout.Infinite);

            //var stateTimer = new Timer(statusChecker.CheckStatus,
            //                       autoEvent, 1000, 250);

            Stc.ws = new WebSocketServer(Stc.wsPort);
            Stc.ws.AddWebSocketService<Ws.MhMainPath>(Stc.wsPath);
            Ws.wsConnManager.Init();

            System.Timers.Timer tm = new System.Timers.Timer();
            tm.Interval = 5 * 60 * 1000;
            tm.Elapsed += Tm_Elapsed;
            tm.Start();


            //Stc.ws.Start();
            //Console.WriteLine($"Servis başlatıldı. Port :{Stc.Port} Path:{Stc.Path}");

            // Start OWIN host 
            StartOptions options = new StartOptions();

            options.Urls.Add("http://localhost:9000");
            options.Urls.Add("http://127.0.0.1:9000");
            options.Urls.Add("http://192.168.0.102:9000");
            options.Urls.Add(string.Format("http://{0}:9000", Environment.MachineName));
            using (WebApp.Start<Startup>(Stc.baseAddress))
            {
                //// Create HttpCient and make a request to api/values 
                //HttpClient client = new HttpClient();

                //var response = client.GetAsync(Stc.baseAddress + "/values").Result;

                //Console.WriteLine(response);
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine("Servis started");
                Console.ReadLine();
            }
        }

        private static void Tm_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Stc.ws.Stop();
                Stc.ws = null;

                Stc.ws = new WebSocketServer(Stc.wsPort);
                Stc.ws.AddWebSocketService<Ws.MhMainPath>(Stc.wsPath);
                Ws.wsConnManager.Init();
                Stc.ws.Start();
            }
            catch (Exception ex)
            {
            }
        }

        static void Callback(Object state)
        {
            // Long running operation
            //_timer.Change(TIME_INTERVAL_IN_MILLISECONDS, Timeout.Infinite);
        }
    }   
}