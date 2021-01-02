namespace Mh.Service.Dto.Notification.Preference
{
    public class NotificationPreferenceListSubDto
    {
        public long Id { get; set; }
        public Enums.NotificationChannels NotificationChannelId { get; set; }

        public bool Preference { get; set; }
    }
}