namespace Mh.Business.Bo.Person.Shop
{
    public class ShopOrderAreaSubListBo
    {
        public int Id { get; set; }

        public string AddressName { get; set; }       

        public decimal OrderMinPrice { get; set; }
        public Enums.OrderDeliveryTimes OrderDeliveryTimeId { get; set; }

        public bool HasStates { get; set; } // this field is used to deticate if that country has states. Especially useful for presentation layer (web).
    }
}