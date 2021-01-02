using System.Collections.Generic;

namespace Mh.Business.Bo.Notification.EMail
{
    public class NotificationEMailListBo
    {
        public long Id { get; set; }
        public string Sender { get; set; }

        public string Subject { get; set; }
        public Enums.EMailSubjectTypes SubjectTypeId { get; set; }
        public string Content { get; set; }
        public bool IsContentHtml { get; set; }

        public long? SenderPersonId { get; set; }
        public Enums.Languages LanguageId { get; set; }
        public string SenderDisplayName { get; set; }

        public List<NotificationEMailReceiverListBo> ReceiverList { get; set; }
        public List<NotificationEMailAttachListBo> AttachList { get; set; }

        public string ReceiverListJson { get; set; }
        public string AttachListJson { get; set; }
    }
}