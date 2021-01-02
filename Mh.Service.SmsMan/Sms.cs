using Mh.Business.Bo.Notification.Sms;
using Mh.Business.Bo.Sys;
using Mh.Business.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Service.SmsMan
{
    public class Sms
    {
        public void Send(NotificationSmsListBo smsBo)
        {
            if (Stc.StartedList.Count(x => x == smsBo.Id) > 0) return;

            //if (smsBo.ReceiverList.Count(x => x.Receiver != "ismail.sarikaya@elmasium.com") > 0) return;
            Stc.StartedList.Add(smsBo.Id);
            SaveLog(smsBo, Enums.SmsLogEvents.xStarted, null, null, true);
            

            try
            {
                Business.Sys.SysBusiness sysBusiness = new Business.Sys.SysBusiness();
                SysSmsBo sysSmsBo = Stc.SysSmsList.FirstOrDefault(x => x.Id == 0);

                //string displayName = "Elmasium - " + Business.Stc.GetDicValue(sysSmsBo.DisplayName, smsBo.LanguageId);
                //if (smsBo.SubjectTypeId == Enums.EMailSubjectTypes.xWelcome)
                //    displayName = Business.Stc.GetDicValue("xWelcomeElmasium", smsBo.LanguageId);

                smsBo.TextMessage = Business.Stc.DictionaryProcessText(smsBo.TextMessage, smsBo.LanguageId);

                ISmsService smsService = null;
                if (sysSmsBo.Id == 0)
                {
                    smsService = new OutService.NetgsmService();
                }

                smsService.UrlAddress = sysSmsBo.UrlAddress;
                smsService.OtpUrlAddress = sysSmsBo.OtpUrlAddress;

                smsService.Username = sysSmsBo.Username;
                smsService.Password = sysSmsBo.Password;

                smsService.CompanyName = sysSmsBo.CompanyName;

                smsService.Caption = smsBo.Caption;
                smsService.TextMessage = smsBo.TextMessage;

                smsService.IsSupportTr = true;

                smsService.PhoneNumberList = new List<string>();
                foreach (NotificationSmsReceiverListBo item in smsBo.ReceiverList)
                {
                    smsService.PhoneNumberList.Add("0" + item.Receiver);
                }

                ServiceReturnBo returnBo = smsService.Send();

                SaveLog(smsBo, Enums.SmsLogEvents.xTransactionSuccessful, null, returnBo.ReturnValue, returnBo.IsSuccess);

                SaveSent(smsBo, true);
            }
            catch (Exception ex)
            {
                ResponseBo exResponseBo = SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name);

                SaveLog(smsBo, Enums.SmsLogEvents.xUnexpectedErrorOccurred, exResponseBo.ReturnedId, null, true);
                SaveSent(smsBo, false);
            }
        }

        void SaveLog(NotificationSmsListBo smsBo, Enums.SmsLogEvents smsEventId, long? logExceptionId, string returnValue, bool isSuccess)
        {
            NotificationSmsBusiness smsBusiness = new NotificationSmsBusiness();
            smsBusiness.SaveLog(new NotificationSmsLogBo()
            {
                NotificationSmsId = smsBo.Id,

                SmsEventId = smsEventId,
                LogExceptionId = logExceptionId,

                ReturnValue = returnValue,
                IsSuccess = isSuccess
            });
        }
        void SaveSent(NotificationSmsListBo smsBo, bool sentSuccessfully)
        {
            NotificationSmsBusiness smsBusiness = new NotificationSmsBusiness();
            smsBusiness.SaveSent(new NotificationSmsSentSaveBo()
            {
                NotificationSmsId = smsBo.Id,

                TextMessage = smsBo.TextMessage,

                SentSuccessfully = sentSuccessfully
            });
        }

        ResponseBo SaveExLog(Exception exception, Type type, string methodName)
        {
            Business.BaseBusiness baseBusiness = new Business.BaseBusiness();

            return baseBusiness.SaveExLog(exception, type, methodName, null, Enums.ApplicationTypes.MhServiceSmsMan);
        }
    }
}