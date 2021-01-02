using System.Collections.Generic;

namespace Mh.Service.Dto.Notification
{
    public class NotificationSeenDto
    {
        public long MyPersonId { get; set; }
        public List<long> NotificationIdList { get; set; }
    }
}