using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Table
{
    public class PersonTableGroupBo : BaseBo
    {
        public long? Id { get; set; }
        public long PersonId { get; set; }

        public string Name { get; set; }
        public Enums.PersonTableGroupStats PersonTableGroupStatId { get; set; }
        public int Order { get; set; }
        public string Notes { get; set; }
    }
}