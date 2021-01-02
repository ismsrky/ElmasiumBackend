namespace Mh.Service.Dto.Person.Table
{
    public class PersonTableDto
    {
        public long? Id { get; set; }

        public long GroupId { get; set; }

        public string Name { get; set; }
        public Enums.PersonTableStats PersonTableStatId { get; set; }
        public int Order { get; set; }
        public string Notes { get; set; }
    }
}