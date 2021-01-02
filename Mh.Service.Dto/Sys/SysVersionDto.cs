namespace Mh.Service.Dto.Sys
{
    public class SysVersionDto
    {
        public int Id { get; set; }
        public Enums.ApplicationTypes ApplicationTypeId { get; set; }
        public int Version { get; set; }
        public double? ReleaseDateNumber { get; set; }
        public string ReleaseNote { get; set; } // not null. Lenght is max.
    }
}