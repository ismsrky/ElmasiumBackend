using System;

namespace Mh.Business.Bo.Notification.EMail
{
    public class NotificationEMailLogBo
    {
        public long NotificationEMailId { get; set; }
        public Enums.EMailLogEvents EmailEventId { get; set; }
        public long? LogExceptionId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}