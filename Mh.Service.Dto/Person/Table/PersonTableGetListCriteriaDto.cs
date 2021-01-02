namespace Mh.Service.Dto.Person.Table
{
    public class PersonTableGetListCriteriaDto
    {
        public long GroupId { get; set; }

        public Enums.PersonTableStats? PersonTableStatId { get; set; } // null means all
    }
}