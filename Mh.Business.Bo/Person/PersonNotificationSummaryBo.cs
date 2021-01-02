namespace Mh.Business.Bo.Person
{
    public class PersonNotificationSummaryBo
    {
        public long PersonId { get; set; }

        public int xFicheIncomings { get; set; }
        public int xFicheOutgoings { get; set; }

        public int xRelationIncomings { get; set; }
        public int xRelationOutgoings { get; set; }

        public int xNotifications { get; set; }

        public int xIncomingOrders { get; set; }
        public int xOutgoingOrders { get; set; }

        public int xIncomingOrderReturns { get; set; }
        public int xOutgoingOrderReturns { get; set; }

        public int xBasket { get; set; }
    }
}