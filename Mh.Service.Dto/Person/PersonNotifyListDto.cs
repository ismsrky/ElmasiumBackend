namespace Mh.Service.Dto.Person
{
    public class PersonNotifyListDto
    {
        public long PersonId { get; set; }
        public Enums.WsNotificationGroupTypes WsNotificationGroupTypeId { get; set; }
    }
}