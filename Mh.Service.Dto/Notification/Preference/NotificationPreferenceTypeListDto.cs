using System.Collections.Generic;

namespace Mh.Service.Dto.Notification.Preference
{
    public class NotificationPreferenceTypeListDto
    {
        public Enums.NotificationPreferenceTypes NotificationPreferenceTypeId { get; set; }
        public List<NotificationPreferenceListSubDto> SubList { get; set; }
    }
}