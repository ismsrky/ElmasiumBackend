using Mh.Business.Bo.Order;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Order;
using Mh.Service.Dto.IncludeExclude;
using Mh.Service.Dto.Option;
using Mh.Service.Dto.Order;
using Mh.Service.Dto.Order.Product;
using Mh.Service.Dto.Product.Code;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mh.Service.WebApi.Order
{
    public class OrderController : BaseController
    {
        readonly IOrderBusiness orderBusiness;
        public OrderController(IOrderBusiness _orderBusiness)
        {
            orderBusiness = _orderBusiness;
        }

        [HttpPost]
        public ResponseDto Save(OrderSaveDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            OrderSaveBo saveBo = new OrderSaveBo()
            {
                BasketId = saveDto.BasketId,

                DeliveryAddressId = saveDto.DeliveryAddressId,

                Notes = saveDto.Notes,

                Session = Session
            };

            ResponseBo responseBo = orderBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            base.SendNotifyWsToList(responseBo.PersonNotifyList);

            return responseDto;
        }

        [HttpPost]
        public ResponseDto SaveReturn(OrderReturnSaveDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            OrderReturnSaveBo saveBo = new OrderReturnSaveBo()
            {
                OrderId = saveDto.OrderId,
                Notes = saveDto.Notes,

                Session = Session
            };

            ResponseBo responseBo = orderBusiness.SaveReturn(saveBo);
            responseDto = responseBo.ToResponseDto();

            base.SendNotifyWsToList(responseBo.PersonNotifyList);

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<OrderListDto>> GetList(OrderGetListCriteriaDto criteriaDto)
        {
            OrderGetListCriteriaBo criteriaBo = new OrderGetListCriteriaBo()
            {
                CaseId =criteriaDto.CaseId,

                PersonId = criteriaDto.PersonId,

                GetIncomings = criteriaDto.GetIncomings,
                GetReturns = criteriaDto.GetReturns,
                OrderStatList = criteriaDto.OrderStatList,

                CurrencyId = criteriaDto.CurrencyId,

                PageOffSet = criteriaDto.PageOffSet,

                Session = Session
            };

            ResponseBo<List<OrderListBo>> responseBo = orderBusiness.GetList(criteriaBo);

            ResponseDto<List<OrderListDto>> responseDto = responseBo.ToResponseDto<List<OrderListDto>, List<OrderListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = responseBo.Bo.
                   GroupBy(x => x.Id).Select(x => x.First()).
                   Select(
                   x => new OrderListDto()
                   {
                       Id = x.Id,

                       DebtPersonId = x.DebtPersonId,
                       DebtPersonFullName = x.DebtPersonFullName,
                       DebtPersonTypeId = x.DebtPersonTypeId,

                       CreditPersonId = x.CreditPersonId,
                       CreditPersonFullName = x.CreditPersonFullName,
                       CreditPersonTypeId = x.CreditPersonTypeId,

                       OrderStatId = x.OrderStatId,
                       CurrencyId = x.CurrencyId,
                       GrandTotal = x.GrandTotal,

                       Notes = x.Notes,

                       IsReturn = x.IsReturn,
                       RelatedOrderId = x.RelatedOrderId,

                       BasketId = x.BasketId,
                       DeliveryAddressId = x.DeliveryAddressId,

                       ShopId = x.ShopId,
                       ShopFullName = x.ShopFullName,
                       ShopStarCount = x.ShopStarCount,
                       ShopStarSum = x.ShopStarSum,
                       ShopUrlName = x.ShopUrlName,

                       CommentId = x.PersonCommentId,
                       IsCommentable = x.IsCommentable,

                       CreateDateNumber = x.CreateDate.ToNumberFromDateTime(),
                       UpdateDateNumber = x.UpdateDate.ToNumberFromDateTimeNull(),

                       Phone = x.Phone,

                       ProductList = responseBo.Bo.Where(y => y.Id == x.Id).Select(
                               y => new OrderProductListDto()
                               {
                                   Id = y.OrderProductId,
                                   Quantity = y.Quantity,
                                   UnitPrice = y.UnitPrice,
                                   GrandTotal = y.RowGrandTotal,
                                   Notes = y.RowNotes,

                                   ProductId = y.ProductId,
                                   ProductName = y.ProductName,
                                   ProductTypeId = y.ProductTypeId,

                                   CategoryId = y.CategoryId,
                                   StarCount = y.StarCount,
                                   StarSum = y.StarSum,

                                   PortraitImageUniqueIdStr = base.GetImageName(y.PortraitImageUniqueId, y.PortraitImageFileTypeId),

                                   CommentId = y.PersonProductCommentId,

                                   CodeList = (from z in y.CodeList
                                               select new ProductCodeDto()
                                               {
                                                   Code = z.Code,
                                                   ProductCodeTypeId = z.ProductCodeTypeId,
                                                   ProductId = z.ProductId
                                               }).ToList(),

                                   OptionList = y.OptionList == null ? null :
                                                (from o in y.OptionList
                                                 select new OptionListDto()
                                                 {
                                                     Id = o.OptionId,
                                                     Name = o.OptionName,
                                                     PriceGap = o.OptionPriceGap
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

        [HttpPost]
        public ResponseDto<List<OrderStatNextListDto>> GetStatNextList()
        {
            ResponseBo<List<OrderStatNextListBo>> responseBo = orderBusiness.GetStatNextList(base.ToBaseBo());

            ResponseDto<List<OrderStatNextListDto>> responseDto = responseBo.ToResponseDto<List<OrderStatNextListDto>, List<OrderStatNextListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = responseBo.Bo.
                   Select(
                   x => new OrderStatNextListDto()
                   {
                       Id = x.Id,

                       OrderStatId = x.OrderStatId,
                       NextOrderStatId = x.NextOrderStatId,

                       ForDebt = x.ForDebt,
                       ForReturn = x.ForReturn
                   }).ToList();
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<OrderStatListDto>> GetStatList()
        {
            ResponseBo<List<OrderStatListBo>> responseBo = orderBusiness.GetStatList(base.ToBaseBo());

            ResponseDto<List<OrderStatListDto>> responseDto = responseBo.ToResponseDto<List<OrderStatListDto>, List<OrderStatListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = responseBo.Bo.
                   Select(
                   x => new OrderStatListDto()
                   {
                       Id = x.Id,

                       Name = x.Name,
                       ActionName = x.ActionName,

                       IsEndPoint = x.IsEndPoint,
                       IsRequireNotes = x.IsRequireNotes,
                       IsRequireAccountTypeId = x.IsRequireAccountTypeId,

                       ColorClassName = x.ColorClassName,
                       IconName = x.IconName,
                       Order = x.Order
                   }).ToList();
            }

            return responseDto;
        }
    }
}