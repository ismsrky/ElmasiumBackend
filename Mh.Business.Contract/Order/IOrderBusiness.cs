using Mh.Business.Bo.Order;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Order
{
    public interface IOrderBusiness
    {
        ResponseBo Save(OrderSaveBo saveBo);
        ResponseBo SaveReturn(OrderReturnSaveBo saveBo);

        ResponseBo<List<OrderListBo>> GetList(OrderGetListCriteriaBo criteriaBo);

        ResponseBo<List<OrderStatNextListBo>> GetStatNextList(BaseBo baseBo);
        ResponseBo<List<OrderStatListBo>> GetStatList(BaseBo baseBo);
    }
}