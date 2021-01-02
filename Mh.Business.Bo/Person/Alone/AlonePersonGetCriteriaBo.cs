using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Alone
{
    public class AlonePersonGetCriteriaBo : BaseBo
    {
        public long PersonId { get; set; }
        public long ParentRelationPersonId { get; set; }
    }
}