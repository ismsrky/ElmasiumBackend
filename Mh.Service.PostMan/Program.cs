using Mh.Business.Bo.Dictionary;
using Mh.Business.Bo.Notification.EMail;
using Mh.Business.Bo.Sys;
using Mh.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mh.Service.PostMan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();
            //string naber = timeZones[10].ToString();

            IronPdf.License.LicenseKey = "IRONPDF-1EF01-118677-74BF79-A5392C3577-37E95A5C-NEx-1EF01";

            Stc.StartedList = new List<long>();

            Encryption.Key = new byte[] { 28, 14, 79, 24, 168, 2, 142, 10, 198, 6,
                89, 93, 115, 12, 253, 71, 62, 221, 55, 121, 38, 174, 6, 51, 168, 54, 2, 26, 228, 113, 32, 109 };
            Encryption.Vector = new byte[] { 195, 74, 41, 17, 219, 10, 64, 41, 81, 39, 209, 165, 4, 86, 236, 88 };

            Stc.ReadConfigs(); //Reads app.config file
            Business.Stc.ConnStr = Stc.ConnStr;

            //string reportService = Encryption.DecryptString("183070049171182237226106168064219126098169172105");
            //string remindService = Encryption.DecryptString("145014132246120143136238214020201124174180138135");
            //string notifyService = Encryption.DecryptString("067092194112154010003231169086207040039193099010");

            // Dictionaries
            Business.Dictionary.DictionaryBusiness dictionaryBusiness = new Business.Dictionary.DictionaryBusiness();
            ResponseBo<List<DictionaryBo>> responseDic = dictionaryBusiness.GetList();
            Business.Stc.DicItemList = responseDic.Bo.Select(x => x.ToDicItem()).ToList();

            Business.Sys.SysBusiness sysBusiness = new Business.Sys.SysBusiness();
            Stc.SysMailList = sysBusiness.GetMailList().Bo;

            System.Timers.Timer tmSendMail = new System.Timers.Timer();
            tmSendMail.Interval = 5000;
            tmSendMail.Elapsed += tmSendMail_Elapsed;
            tmSendMail.Start();

            System.Timers.Timer tmSchedule = new System.Timers.Timer();
            tmSchedule.Interval = 1000;
            tmSchedule.Elapsed += tmSchedule_Elapsed;
            tmSchedule.Start();

            Console.ReadKey(true);
        }

        private static void tmSendMail_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Business.Notification.NotificationEMailBusiness eMailBusiness = new Business.Notification.NotificationEMailBusiness();
            ResponseBo<List<NotificationEMailListBo>> responseListBo = eMailBusiness.GetNotSentList();

            if (responseListBo.IsSuccess && responseListBo.Bo != null && responseListBo.Bo.Count() > 0)
            {
                foreach (NotificationEMailListBo item in responseListBo.Bo)
                {
                    EMail eMail = new EMail();
                    eMail.Send(item);
                }
            }
        }

        static bool workingSchedule = false;
        private static void tmSchedule_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (workingSchedule) return;

            try
            {
                if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                {
                    workingSchedule = true;

                    Business.Notification.NotificationEMailBusiness eMailBusiness = new Business.Notification.NotificationEMailBusiness();
                    ResponseBo responseBo = eMailBusiness.PrepareDailySummary();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                workingSchedule = false;
            }
        }
    }
}

//string a = Mh.Utils.Encryption.EncryptToString("R*aso4154!");
//string b = Mh.Utils.Encryption.EncryptToString("K.mpz4H67,");
//string c = Mh.Utils.Encryption.EncryptToString("8pN-.5YJs**");