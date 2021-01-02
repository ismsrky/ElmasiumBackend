using System.Collections.Generic;

namespace Mh.Service.Dto.Notification.Preference
{
    public class NotificationPreferenceListDto
    {
        public long RelatedPersonId { get; set; }
        public string RelatedPersonFullName { get; set; }
        public Enums.PersonTypes RelatedPersonTypeId { get; set; }

        public Enums.RelationTypes RelationTypeId { get; set; }

        public List<NotificationPreferenceTypeListDto> TypeList { get; set; }
    }
}