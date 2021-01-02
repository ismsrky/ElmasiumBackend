using System;

namespace Mh.Business.Bo.Dashboard.Slider
{
    public class DashboardSliderListBo
    {
        public int Id { get; set; }
        public int Order { get; set; }

        public string xText { get; set; }
        public string xTextBold { get; set; }
        public string xButtonText { get; set; }

        public string ColorClass { get; set; }

        public Guid? PortraitImageUniqueId { get; set; }
        public Enums.FileTypes? PortraitImageFileTypeId { get; set; }

        public int GroupId { get; set; }
    }
}