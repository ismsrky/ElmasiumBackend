using System;

namespace Mh.Business.Bo.Notification.Sms
{
    public class NotificationSmsLogBo
    {
        public long NotificationSmsId { get; set; }
        public Enums.SmsLogEvents SmsEventId { get; set; }
        public long? LogExceptionId { get; set; }
        public string ReturnValue { get; set; }
        public bool IsSuccess { get; set; }
    }
}