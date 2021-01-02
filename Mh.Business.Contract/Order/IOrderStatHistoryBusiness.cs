using Mh.Business.Bo.Order.StatHistory;
using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Contract.Order
{
    public interface IOrderStatHistoryBusiness
    {
        ResponseBo Save(OrderStatHistoryBo saveBo);
        ResponseBo<OrderStatHistoryBo> Get(OrderStatHistoryGetCriteriaBo criteriaBo);
        ResponseBo<List<OrderStatHistoryListBo>> GetList(OrderStatHistoryGetListCriteriaBo criteriaBo);
    }
}