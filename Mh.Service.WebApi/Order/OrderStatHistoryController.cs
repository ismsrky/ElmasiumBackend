using Mh.Business.Bo.Order.StatHistory;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Order;
using Mh.Service.Dto.Order.StatHistory;
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
    public class OrderStatHistoryController : BaseController
    {
        readonly IOrderStatHistoryBusiness orderStatHistoryBusiness;
        public OrderStatHistoryController(IOrderStatHistoryBusiness _orderStatHistoryBusiness)
        {
            orderStatHistoryBusiness = _orderStatHistoryBusiness;
        }

        [HttpPost]
        public ResponseDto Save(OrderStatHistoryDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            OrderStatHistoryBo saveBo = new OrderStatHistoryBo()
            {
                OrderId = saveDto.OrderId,
                OrderStatId = saveDto.OrderStatId,

                PersonId = saveDto.PersonId,

                Notes = saveDto.Notes,

                AccountTypeId = saveDto.AccountTypeId,

                Session = Session
            };

            ResponseBo responseBo = orderStatHistoryBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            base.SendNotifyWsToList(responseBo.PersonNotifyList);

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<OrderStatHistoryDto> Get(OrderStatHistoryGetCriteriaDto criteriaDto)
        {
            OrderStatHistoryGetCriteriaBo criteriaBo = new OrderStatHistoryGetCriteriaBo()
            {
                Id = criteriaDto.Id,

                Session = Session
            };

            ResponseBo<OrderStatHistoryBo> responseBo = orderStatHistoryBusiness.Get(criteriaBo);

            ResponseDto<OrderStatHistoryDto> responseDto = responseBo.ToResponseDto<OrderStatHistoryDto, OrderStatHistoryBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new OrderStatHistoryDto()
                {
                    Id = responseBo.Bo.Id,

                    OrderId = responseBo.Bo.OrderId,
                    OrderStatId = responseBo.Bo.OrderStatId,

                    PersonId = responseBo.Bo.PersonId,

                    Notes = responseBo.Bo.Notes,

                    AccountTypeId = responseBo.Bo.AccountTypeId,

                    CreateDateNumber = responseBo.Bo.CreateDate.ToNumberFromDateTime()
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<OrderStatHistoryListDto>> GetList(OrderStatHistoryGetListCriteriaDto criteriaDto)
        {
            OrderStatHistoryGetListCriteriaBo criteriaBo = new OrderStatHistoryGetListCriteriaBo()
            {
                OrderId = criteriaDto.OrderId,

                Session = Session
            };

            ResponseBo<List<OrderStatHistoryListBo>> responseBo = orderStatHistoryBusiness.GetList(criteriaBo);

            ResponseDto<List<OrderStatHistoryListDto>> responseDto = responseBo.ToResponseDto<List<OrderStatHistoryListDto>, List<OrderStatHistoryListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<OrderStatHistoryListDto>();
                foreach (OrderStatHistoryListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new OrderStatHistoryListDto()
                    {
                        Id = itemBo.Id,
                        OrderStatId=itemBo.OrderStatId,

                        ParentPersonId = itemBo.ParentPersonId,
                        ParentPersonFullName = itemBo.ParentPersonFullName,

                        Notes = itemBo.Notes,
                        CreateDateNumber = itemBo.CreateDate.ToNumberFromDateTime()
                    });
                }
            }

            return responseDto;
        }
    }
}