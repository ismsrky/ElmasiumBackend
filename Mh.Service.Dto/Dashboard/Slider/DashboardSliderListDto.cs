namespace Mh.Service.Dto.Dashboard.Slider
{
    public class DashboardSliderListDto
    {
        public int Id { get; set; }
        public int Order { get; set; }

        public string xText { get; set; }
        public string xTextBold { get; set; }
        public string xButtonText { get; set; }

        public string ColorClass { get; set; }

        public string PortraitImageUniqueIdStr { get; set; }

        public int GroupId { get; set; }
    }
}