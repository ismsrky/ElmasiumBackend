using Mh.Business.Bo.Basket.Product;
using Mh.Business.Bo.Sys;

namespace Mh.Business.Contract.Basket
{
    public interface IBasketProductBusiness
    {
        ResponseBo Save(BasketProductSaveBo saveBo);
        ResponseBo UpdateQuantity(BasketProductQuantityUpdateBo updateBo);

        ResponseBo Delete(BasketProductDeleteBo deleteBo);

        ResponseBo UpdateOption(BasketProductOptionUpdateBo updateBo);
        ResponseBo UpdateIncludeExclude(BasketProductIncludeExcludeUpdateBo updateBo);
    }
}