using Mh.Business.Bo.Person.Product;
using Mh.Business.Bo.Property;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Property
{
    public interface IPropertyBusiness
    {
        ResponseBo<List<PropertyListBo>> GetList(PropertyGetListCriteriaBo criteriaBo);
        ResponseBo<List<PropertyGroupListBo>> GetGroupList(PropertyGroupGetListCriteriaBo criteriaBo);

        ResponseBo Save(PropertySaveBo saveBo);
        ResponseBo SavePersonProduct(PersonProductPropertyBo saveBo);
        ResponseBo DeletePersonProduct(PersonProductPropertyBo deleteBo);
    }
}