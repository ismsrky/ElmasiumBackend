using Mh.Business.Bo.Notification.EMail;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Notification
{
    public interface INotificationEMailBusiness
    {
        ResponseBo<List<NotificationEMailListBo>> GetNotSentList();

        ResponseBo SaveSent(NotificationEMailSentSaveBo saveBo);

        ResponseBo SaveLog(NotificationEMailLogBo saveBo);
    }
}