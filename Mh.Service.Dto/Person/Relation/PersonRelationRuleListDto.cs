namespace Mh.Service.Dto.Person.Relation
{
    public class PersonRelationRuleListDto
    {
        public int Id { get; set; }
        public Enums.PersonTypes ParentPersonTypeId { get; set; }
        public Enums.PersonTypes ChildPersonTypeId { get; set; }
        public Enums.RelationTypes ParentRelationTypeId { get; set; }
        public Enums.RelationTypes ChildRelationTypeId { get; set; }
    }
}