namespace Mh.Business.Bo.Person
{
    public class PersonNavMenuBo
    {
        public int Id { get; set; } // not null
        public int ParentId { get; set; } // not null
        public string Name { get; set; } // not null
        public string Url { get; set; } // null
        public string IconClass { get; set; } // null
        public string IconColor { get; set; } // null

        public int Range { get; set; } // not null
        public int RangeOrder { get; set; } // not null

        public Enums.NavMenuPositions Position { get; set; }
    }
}