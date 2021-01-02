using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Help
{
    public class HelpGetListCriteriaBo : BaseBo
    {
        public Enums.ApplicationTypes ApplicationTypeId { get; set; } // not null
        public string Name { get; set; } // not null
    }
}