using Mh.Business.Bo.Notification;
using Mh.Business.Bo.Person.Real;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Notification;
using Mh.Service.Dto.Notification;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System.Collections.Generic;
using System.Web.Http;


namespace Mh.Service.WebApi.Notification
{
    public class NotificationController : BaseController
    {
        readonly INotificationBusiness notificationBusiness;
        public NotificationController(INotificationBusiness _notificationBusiness)
        {
            notificationBusiness = _notificationBusiness;
        }

        [HttpPost]
        public ResponseDto<List<NotificationListDto>> GetList(NotificationGetListCriteriaDto criteriaDto)
        {
            NotificationGetListCriteriaBo criteriaBo = new NotificationGetListCriteriaBo()
            {
                MyPersonId = criteriaDto.MyPersonId,
                PageOffSet = criteriaDto.PageOffSet,

                Session = Session
            };

            ResponseBo<List<NotificationListBo>> responseBo = notificationBusiness.GetList(criteriaBo);

            ResponseDto<List<NotificationListDto>> responseDto = responseBo.ToResponseDto<List<NotificationListDto>, List<NotificationListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<NotificationListDto>();
                foreach (NotificationListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new NotificationListDto()
                    {
                        NotificationId = itemBo.NotificationId,
                        NotificationTypeId = itemBo.NotificationTypeId,

                        ParentRelationTypeId = itemBo.ParentRelationTypeId,
                        ChildRelationTypeId = itemBo.ChildRelationTypeId,

                        ParentPersonId = itemBo.ParentPersonId,
                        ParentPersonTypeId = itemBo.ParentPersonTypeId,
                        ParentPersonFullName = itemBo.ParentPersonFullName,

                        ChildPersonId = itemBo.ChildPersonId,
                        ChildPersonTypeId = itemBo.ChildPersonTypeId,
                        ChildPersonFullName = itemBo.ChildPersonFullName,

                        ApprovalStatId = itemBo.ApprovalStatId,

                        FicheId = itemBo.FicheId,
                        FicheTypeId = itemBo.FicheTypeId,
                        FicheGrandTotal = itemBo.FicheGrandTotal,
                        FicheCurrencyId = itemBo.FicheCurrencyId,
                        FicheTypeFakeId = itemBo.FicheTypeFakeId,

                        IsParentDebt = itemBo.IsParentDebt,

                        OrderId = itemBo.OrderId,
                        OrderStatId = itemBo.OrderStatId,
                        OrderGrandTotal = itemBo.OrderGrandTotal,
                        OrderCurrencyId = itemBo.OrderCurrencyId,
                        OrderIsReturn = itemBo.OrderIsReturn,
                        RelatedOrderId = itemBo.RelatedOrderId,

                        CreateDateNumber = itemBo.CreateDate.ToNumberFromDateTime(),

                        IsSeen = itemBo.IsSeen
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Seen(NotificationSeenDto seenDto)
        {
            NotificationSeenBo seenBo = new NotificationSeenBo()
            {
                MyPersonId = seenDto.MyPersonId,
                NotificationIdList = seenDto.NotificationIdList,

                Session = Session
            };

            ResponseBo responseBo = notificationBusiness.Seen(seenBo);

            base.SendNotifyWsToList(responseBo.PersonNotifyList);

            return responseBo.ToResponseDto();
        }
    }
}