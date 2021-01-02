using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Table
{
    public class PersonTableGroupGetListCriteriaBo : BaseBo
    {
        public long PersonId { get; set; }

        public Enums.PersonTableGroupStats? PersonTableGroupStatId { get; set; } // null means all
    }
}