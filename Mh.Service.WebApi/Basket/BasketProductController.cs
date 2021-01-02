using Mh.Business.Bo.Basket.Product;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Basket;
using Mh.Service.Dto.Basket.Product;
using Mh.Service.Dto.Sys;
using System.Web.Http;

namespace Mh.Service.WebApi.Basket
{
    public class BasketProductController : BaseController
    {
        readonly IBasketProductBusiness basketProductBusiness;
        public BasketProductController(IBasketProductBusiness _basketProductBusiness)
        {
            basketProductBusiness = _basketProductBusiness;
        }

        [HttpPost]
        public ResponseDto Save(BasketProductSaveDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            BasketProductSaveBo saveBo = new BasketProductSaveBo()
            {
                DebtPersonId = saveDto.DebtPersonId,
                CreditPersonId = saveDto.CreditPersonId,

                ProductId = saveDto.ProductId,
                Quantity = saveDto.Quantity,

                CurrencyId = saveDto.CurrencyId,
                IsFastSale = saveDto.IsFastSale,

                OptionIdList = saveDto.OptionIdList,
                IncludeExcludeIdList = saveDto.IncludeExcludeIdList,

                Session = Session
            };

            ResponseBo responseBo = basketProductBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            base.SendNotifyWsToList(responseBo.PersonNotifyList);

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Delete(BasketProductDeleteDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            BasketProductDeleteBo saveBo = new BasketProductDeleteBo()
            {
                BasketProductId = deleteDto.BasketProductId,

                Session = Session
            };

            ResponseBo responseBo = basketProductBusiness.Delete(saveBo);
            responseDto = responseBo.ToResponseDto();

            base.SendNotifyWsToList(responseBo.PersonNotifyList);

            return responseDto;
        }

        [HttpPost]
        public ResponseDto UpdateQuantity(BasketProductQuantityUpdateDto updateDto)
        {
            ResponseDto responseDto = new ResponseDto();

            BasketProductQuantityUpdateBo updateBo = new BasketProductQuantityUpdateBo()
            {
                BasketProductId = updateDto.BasketProductId,
                Quantity= updateDto.Quantity,

                Session = Session
            };

            ResponseBo responseBo = basketProductBusiness.UpdateQuantity(updateBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto UpdateOption(BasketProductOptionUpdateDto updateDto)
        {
            ResponseDto responseDto = new ResponseDto();

            BasketProductOptionUpdateBo updateBo = new BasketProductOptionUpdateBo()
            {
                BasketProductId = updateDto.BasketProductId,
                OptionIdList = updateDto.OptionIdList,

                Session = Session
            };

            ResponseBo responseBo = basketProductBusiness.UpdateOption(updateBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto UpdateIncludeExclude(BasketProductIncludeExcludeUpdateDto updateDto)
        {
            ResponseDto responseDto = new ResponseDto();

            BasketProductIncludeExcludeUpdateBo updateBo = new BasketProductIncludeExcludeUpdateBo()
            {
                BasketProductId = updateDto.BasketProductId,
                IncludeExcludeIdList = updateDto.IncludeExcludeIdList,

                Session = Session
            };

            ResponseBo responseBo = basketProductBusiness.UpdateIncludeExclude(updateBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }
    }
}