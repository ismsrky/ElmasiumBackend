namespace Mh.Service.Dto.Notification
{
    public class NotificationListDto
    {
        public long NotificationId { get; set; }
        public Enums.NotificationTypes NotificationTypeId { get; set; }

        public Enums.RelationTypes? ParentRelationTypeId { get; set; }
        public Enums.RelationTypes? ChildRelationTypeId { get; set; }

        public long ParentPersonId { get; set; }
        public Enums.PersonTypes ParentPersonTypeId { get; set; }
        public string ParentPersonFullName { get; set; }

        public long ChildPersonId { get; set; }
        public Enums.PersonTypes ChildPersonTypeId { get; set; }
        public string ChildPersonFullName { get; set; }

        public Enums.ApprovalStats? ApprovalStatId { get; set; }

        public long? FicheId { get; set; }
        public Enums.FicheTypes? FicheTypeId { get; set; }
        public decimal? FicheGrandTotal { get; set; }
        public Enums.Currencies? FicheCurrencyId { get; set; }
        public Enums.FicheTypeFakes? FicheTypeFakeId { get; set; }

        public bool IsParentDebt { get; set; }

        public long? OrderId { get; set; }
        public Enums.OrderStats? OrderStatId { get; set; }
        public decimal? OrderGrandTotal { get; set; }
        public decimal? OrderCurrencyId { get; set; }
        public bool? OrderIsReturn { get; set; }
        public long? RelatedOrderId { get; set; }

        public double CreateDateNumber { get; set; }

        public bool IsSeen { get; set; }
    }
}