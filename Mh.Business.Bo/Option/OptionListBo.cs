namespace Mh.Business.Bo.Option
{
    public class OptionListBo
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupUrlName { get; set; }

        public int OptionId { get; set; }
        public string OptionName { get; set; }
        public string OptionUrlName { get; set; }
        public decimal OptionPriceGap { get; set; } // can be 0, < 0 and > 0
    }
}