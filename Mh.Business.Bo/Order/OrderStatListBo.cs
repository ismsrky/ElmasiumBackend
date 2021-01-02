namespace Mh.Business.Bo.Order
{
    public class OrderStatListBo
    {
        public Enums.OrderStats Id { get; set; }

        public string Name { get; set; } // not null
        public string ActionName { get; set; } // not null

        public bool IsEndPoint { get; set; }
        public bool IsRequireNotes { get; set; }
        public bool IsRequireAccountTypeId { get; set; }

        public string ColorClassName { get; set; } // not null
        public string IconName { get; set; } // not null
        public int Order { get; set; }
    }
}