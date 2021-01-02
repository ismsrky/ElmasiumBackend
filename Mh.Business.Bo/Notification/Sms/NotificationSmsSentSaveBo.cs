namespace Mh.Business.Bo.Notification.Sms
{
    public class NotificationSmsSentSaveBo
    {
        public long NotificationSmsId { get; set; }

        public string TextMessage { get; set; } // translated Content

        public bool SentSuccessfully { get; set; }
    }
}