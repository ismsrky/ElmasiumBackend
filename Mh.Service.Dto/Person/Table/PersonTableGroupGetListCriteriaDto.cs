namespace Mh.Service.Dto.Person.Table
{
    public class PersonTableGroupGetListCriteriaDto
    {
        public long PersonId { get; set; }

        public Enums.PersonTableGroupStats? PersonTableGroupStatId { get; set; } // null means all
    }
}