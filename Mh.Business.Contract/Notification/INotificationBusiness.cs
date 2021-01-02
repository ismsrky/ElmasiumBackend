using Mh.Business.Bo.Notification;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Notification
{
    public interface INotificationBusiness
    {
        ResponseBo<List<NotificationListBo>> GetList(NotificationGetListCriteriaBo criteriaBo);

        ResponseBo Seen(NotificationSeenBo seenBo);
    }
}