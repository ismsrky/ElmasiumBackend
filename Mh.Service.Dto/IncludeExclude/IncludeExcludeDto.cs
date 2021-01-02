namespace Mh.Service.Dto.IncludeExclude
{
    public class IncludeExcludeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceGap { get; set; }

        public bool IsInclude { get; set; }
    }
}