using Mh.Business.Bo.Dictionary;
using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Contract.Dictionary
{
    public interface ILanguageBusiness
    {
        ResponseBo<List<LanguageBo>> GetList();
    }
}