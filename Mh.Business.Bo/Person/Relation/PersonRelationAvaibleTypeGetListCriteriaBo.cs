using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Relation
{
    public class PersonRelationAvaibleTypeGetListCriteriaBo : BaseBo
    {
        public long PersonId { get; set; }

        public Enums.RelationTypes? ChildPersonTypeId { get; set; }

        public bool OnlySearchables { get; set; }
        public bool OnlyMasters { get; set; }
    }
}