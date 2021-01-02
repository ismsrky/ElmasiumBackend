using Mh.Business.Bo.Notification.Sms;
using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Contract.Notification
{
    public interface INotificationSmsBusiness
    {
        ResponseBo<List<NotificationSmsListBo>> GetNotSentList();

        ResponseBo SaveSent(NotificationSmsSentSaveBo saveBo);

        ResponseBo SaveLog(NotificationSmsLogBo saveBo);
    }
}