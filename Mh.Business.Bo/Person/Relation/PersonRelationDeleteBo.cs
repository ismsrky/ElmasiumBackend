using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Relation
{
    public class PersonRelationDeleteBo : BaseBo
    {
        public long PersonRelationId { get; set; }
        public long PersonId { get; set; }
    }
}