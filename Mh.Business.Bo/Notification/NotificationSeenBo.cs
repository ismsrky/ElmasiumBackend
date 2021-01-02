using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Bo.Notification
{
    public class NotificationSeenBo : BaseBo
    {
        public long MyPersonId { get; set; }
        public List<long> NotificationIdList { get; set; }
    }
}