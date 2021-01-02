using Mh.Business.Bo.Dictionary;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Dictionary
{
    public interface IDictionaryBusiness
    {
        ResponseBo<List<DictionaryBo>> GetList();
    }
}