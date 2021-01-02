using Mh.Business.Bo.Basket;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Basket;
using Mh.Service.Dto.Basket;
using Mh.Service.Dto.Basket.Product;
using Mh.Service.Dto.IncludeExclude;
using Mh.Service.Dto.Option;
using Mh.Service.Dto.Product.Code;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mh.Service.WebApi.Basket
{
    public class BasketController : BaseController
    {
        readonly IBasketBusiness basketBusiness;
        public BasketController(IBasketBusiness _basketBusiness)
        {
            basketBusiness = _basketBusiness;
        }

        [HttpPost]
        public ResponseDto Delete(BasketDeleteDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            BasketDeleteBo deleteBo = new BasketDeleteBo()
            {
                BasketId = deleteDto.BasketId,

                Session = Session
            };

            ResponseBo responseBo = basketBusiness.Delete(deleteBo);
            responseDto = responseBo.ToResponseDto();

            base.SendNotifyWsToList(responseBo.PersonNotifyList);

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<BasketListDto>> GetList(BasketGetListCriteriaDto criteriaDto)
        {
            BasketGetListCriteriaBo criteriaBo = new BasketGetListCriteriaBo()
            {
                DebtPersonId = criteriaDto.DebtPersonId,
                CurrencyId = criteriaDto.CurrencyId,
                BasketId = criteriaDto.BasketId,

                Session = Session
            };

            ResponseBo<List<BasketListBo>> responseBo = basketBusiness.GetList(criteriaBo);

            ResponseDto<List<BasketListDto>> responseDto = responseBo.ToResponseDto<List<BasketListDto>, List<BasketListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = responseBo.Bo.
                   GroupBy(x => x.BasketId).Select(x => x.First()).
                   Select(
                   x => new BasketListDto()
                   {
                       Id = x.BasketId,

                       DebtPersonId = x.DebtPersonId,
                       CurrencyId = x.CurrencyId,
                       GrandTotal = x.GrandTotal,

                       ShopId = x.ShopId,
                       ShopFullName = x.ShopFullName,
                       ShopStarCount = x.ShopStarCount,
                       ShopStarSum = x.ShopStarSum,
                       ShopUrlName = x.ShopUrlName,

                       CreateDateNumber = x.CreateDate.ToNumberFromDateTime(),
                       UpdateDateNumber = x.UpdateDate.ToNumberFromDateTimeNull(),

                       ProductList = responseBo.Bo.Where(y => y.BasketId == x.BasketId).Select(
                               y => new BasketProductListDto()
                               {
                                   Id = y.BasketProductId,
                                   Quantity = y.Quantity,
                                   UnitPrice = y.UnitPrice,
                                   GrandTotal = y.RowGrandTotal,
                                   Notes = y.Notes,

                                   ProductId = y.ProductId,
                                   ProductName = y.ProductName,
                                   ProductTypeId = y.ProductTypeId,

                                   PersonProductId = y.PersonProductId,
                                   CategoryId = y.CategoryId,
                                   Balance = y.Balance,
                                   StarCount = y.StarCount,
                                   StarSum = y.StarSum,

                                   PortraitImageUniqueIdStr = base.GetImageName(y.PortraitImageUniqueId, y.PortraitImageFileTypeId),

                                   CodeList = (from z in y.CodeList
                                               select new ProductCodeDto()
                                               {
                                                   Code = z.Code,
                                                   ProductCodeTypeId = z.ProductCodeTypeId,
                                                   ProductId = z.ProductId
                                               }).ToList(),

                                   OptionList = y.OptionList == null ? null :
                                                (from o in y.OptionList
                                                 select new OptionDto()
                                                 {
                                                     OptionId = o.OptionId,
                                                     PriceGap = o.PriceGap
                                                 }).ToList(),
                                   IncludeExcludeList = y.IncludeExcludeList == null ? null :
                                                (from i in y.IncludeExcludeList
                                                 select new IncludeExcludeDto()
                                                 {
                                                     Id = i.Id,
                                                     PriceGap = i.PriceGap,
                                                     Name = i.Name,
                                                     IsInclude = i.IsInclude
                                                 }).ToList()
                               }).ToList()
                   }).ToList();
            }

            return responseDto;
        }
    }
}