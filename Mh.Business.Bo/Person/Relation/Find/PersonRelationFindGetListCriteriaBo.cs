using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Relation.Find
{
    public class PersonRelationFindGetListCriteriaBo : BaseBo
    {
        public long ParentPersonId { get; set; }
        public string Name { get; set; }
        public Enums.RelationTypes RelationTypeId { get; set; }
    }
}