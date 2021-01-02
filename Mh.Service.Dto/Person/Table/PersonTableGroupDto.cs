namespace Mh.Service.Dto.Person.Table
{
    public class PersonTableGroupDto
    {
        public long? Id { get; set; }
        public long PersonId { get; set; }

        public string Name { get; set; }
        public Enums.PersonTableGroupStats PersonTableGroupStatId { get; set; }
        public int Order { get; set; }
        public string Notes { get; set; }
    }
}