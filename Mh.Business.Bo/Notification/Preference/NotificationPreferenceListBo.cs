namespace Mh.Business.Bo.Notification.Preference
{
    public class NotificationPreferenceListBo
    {
        public long Id { get; set; }

        public long RelatedPersonId { get; set; }
        public string RelatedPersonFullName { get; set; }
        public Enums.PersonTypes RelatedPersonTypeId { get; set; }
        public Enums.RelationTypes RelationTypeId { get; set; }

        public Enums.NotificationChannels NotificationChannelId { get; set; }
        public Enums.NotificationPreferenceTypes NotificationPreferenceTypeId { get; set; }

        public bool Preference { get; set; }
    }
}