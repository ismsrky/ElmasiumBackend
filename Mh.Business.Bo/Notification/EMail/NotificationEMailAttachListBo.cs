using System;

namespace Mh.Business.Bo.Notification.EMail
{
    public class NotificationEMailAttachListBo
    {
        public long Id { get; set; }
        public Guid UniqueId { get; set; }
        public Enums.FileTypes FileTypeId { get; set; }

        public string PseudoFileName { get; set; }
        public string HtmlRaw { get; set; }
    }
}