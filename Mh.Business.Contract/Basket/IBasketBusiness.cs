using Mh.Business.Bo.Basket;
using Mh.Business.Bo.Basket.Product;
using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Contract.Basket
{
    public interface IBasketBusiness
    {
        ResponseBo Delete(BasketDeleteBo deleteBo);

        ResponseBo<List<BasketListBo>> GetList(BasketGetListCriteriaBo criteriaBo);
    }
}