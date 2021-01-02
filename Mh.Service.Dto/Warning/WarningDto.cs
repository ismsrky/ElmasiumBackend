namespace Mh.Service.Dto.Warning
{
    public class WarningDto
    {
        public long Id { get; set; }
        public Enums.WarningModuleTypes WarningModuleTypeId { get; set; }

        // One of following params must has a value and others must be null.
        public long? PersonProductId { get; set; }
        public long? CommentId { get; set; }
        public long? PersonId { get; set; }

        public Enums.EnumWarningTypes WarningTypeId { get; set; }
        public string Notes { get; set; }
    }
}