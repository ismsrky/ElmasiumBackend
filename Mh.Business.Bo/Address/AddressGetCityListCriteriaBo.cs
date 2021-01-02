using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Address
{
    public class AddressGetCityListCriteriaBo : BaseBo
    {
        public int CountryId { get; set; }
        public int? StateId { get; set; }
    }
}