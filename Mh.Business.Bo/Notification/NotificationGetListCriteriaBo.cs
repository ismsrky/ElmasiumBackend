using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Notification
{
    public class NotificationGetListCriteriaBo : BaseBo
    {
        public long MyPersonId { get; set; }
        public int PageOffSet { get; set; }
    }
}