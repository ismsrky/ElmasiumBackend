using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Table
{
    public class PersonTableBo : BaseBo
    {
        public long? Id { get; set; }

        public long GroupId { get; set; }

        public string Name { get; set; }
        public Enums.PersonTableStats PersonTableStatId { get; set; }
        public int Order { get; set; }
        public string Notes { get; set; }
    }
}