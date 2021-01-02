using System;
using System.Collections.Generic;

namespace Mh.Business.Bo.Notification.EMail
{
    public class NotificationEMailSentSaveBo
    {
        public long NotificationEMailId { get; set; }

        public string Content { get; set; } // translated Content

        public bool SentSuccessfully { get; set; }
    }
}