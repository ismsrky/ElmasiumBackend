namespace Mh.Service.Dto.Help
{
    public class HelpGetListCriteriaDto
    {
        public Enums.ApplicationTypes ApplicationTypeId { get; set; } // not null
        public string Name { get; set; } // not null
    }
}