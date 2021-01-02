namespace Mh.Service.Dto.Person.Relation
{
    public class PersonRelationAvaibleTypeGetListCriteriaDto
    {
        public long PersonId { get; set; }

        public Enums.RelationTypes? ChildPersonTypeId { get; set; }

        public bool OnlySearchables { get; set; }
        public bool OnlyMasters { get; set; }
    }
}