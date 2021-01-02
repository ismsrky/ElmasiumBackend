using Mh.Business.Bo.Option;
using Mh.Business.Bo.Person.Product;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Option
{
    public interface IOptionBusiness
    {
        ResponseBo<List<OptionListBo>> GetList(OptionGetListCriteriaBo criteriaBo);
        ResponseBo<List<OptionGroupListBo>> GetGroupList(OptionGroupGetListCriteriaBo criteriaBo);
        ResponseBo Save(OptionSaveBo saveBo);
        ResponseBo SavePersonProduct(PersonProductOptionBo saveBo);
    }
}