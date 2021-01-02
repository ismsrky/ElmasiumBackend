using Mh.Business.Bo.Notification.Preference;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Notification
{
    public interface INotificationPreferenceBusiness
    {
        ResponseBo<List<NotificationPreferenceListBo>> GetList(BaseBo baseBo);

        ResponseBo Save(NotificationPreferenceSaveBo saveBo);
    }
}