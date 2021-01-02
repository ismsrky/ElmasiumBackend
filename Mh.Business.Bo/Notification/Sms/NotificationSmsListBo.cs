using System.Collections.Generic;

namespace Mh.Business.Bo.Notification.Sms
{
    public class NotificationSmsListBo
    {
        public long Id { get; set; }

        public string Caption { get; set; }
        public string TextMessage { get; set; }
        public bool IsOtp { get; set; }

        public Enums.Languages LanguageId { get; set; }

        public int NotificationSmsTemplateId { get; set; }

        public List<NotificationSmsReceiverListBo> ReceiverList { get; set; }

        public string ReceiverListJson { get; set; }
    }
}