using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Table
{
    public class PersonTableGetListCriteriaBo : BaseBo
    {
        public long GroupId { get; set; }

        public Enums.PersonTableStats? PersonTableStatId { get; set; } // null means all
    }
}