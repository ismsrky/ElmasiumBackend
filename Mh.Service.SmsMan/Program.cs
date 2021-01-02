using Mh.Business.Bo.Dictionary;
using Mh.Business.Bo.Notification.Sms;
using Mh.Business.Bo.Sys;
using Mh.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Service.SmsMan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stc.StartedList = new List<long>();

            Encryption.Key = new byte[] { 28, 14, 79, 24, 168, 2, 142, 10, 198, 6,
                89, 93, 115, 12, 253, 71, 62, 221, 55, 121, 38, 174, 6, 51, 168, 54, 2, 26, 228, 113, 32, 109 };
            Encryption.Vector = new byte[] { 195, 74, 41, 17, 219, 10, 64, 41, 81, 39, 209, 165, 4, 86, 236, 88 };

            Stc.ReadConfigs(); //Reads app.config file
            Business.Stc.ConnStr = Stc.ConnStr;

            // Dictionaries
            Business.Dictionary.DictionaryBusiness dictionaryBusiness = new Business.Dictionary.DictionaryBusiness();
            ResponseBo<List<DictionaryBo>> responseDic = dictionaryBusiness.GetList();
            Business.Stc.DicItemList = responseDic.Bo.Select(x => x.ToDicItem()).ToList();

            Business.Sys.SysBusiness sysBusiness = new Business.Sys.SysBusiness();
            Stc.SysSmsList = sysBusiness.GetSmsList().Bo;

            System.Timers.Timer tmSendSms = new System.Timers.Timer();
            tmSendSms.Interval = 5000;
            tmSendSms.Elapsed += tmSendSms_Elapsed;
            tmSendSms.Start();

            Console.ReadKey(true);
        }

        private static void tmSendSms_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Business.Notification.NotificationSmsBusiness smsBusiness = new Business.Notification.NotificationSmsBusiness();
            ResponseBo<List<NotificationSmsListBo>> responseListBo = smsBusiness.GetNotSentList();

            if (responseListBo.IsSuccess && responseListBo.Bo != null && responseListBo.Bo.Count() > 0)
            {
                foreach (NotificationSmsListBo item in responseListBo.Bo)
                {
                    Sms sms = new Sms();
                    sms.Send(item);
                }
            }
        }
    }
}