using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Product
{
    public class PersonProductAddInventoryBo : BaseBo
    {
        public long ProductId { get; set; }
        public long PersonId { get; set; } // can be real or shop.
    }
}