namespace Mh.Business.Bo.Notification.EMail
{
    public class NotificationEMailReceiverListBo
    {
        public long Id { get; set; }
        public string Receiver { get; set; }
        public Enums.EMailReceiverTypes ReceiverTypeId { get; set; }
        public long ReceiverPersonId { get; set; }
    }
}