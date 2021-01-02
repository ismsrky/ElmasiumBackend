using Mh.Business.Bo.Notification.Preference;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Notification;
using Mh.Service.Dto.Notification.Preference;
using Mh.Service.Dto.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mh.Service.WebApi.Notification
{
    public class NotificationPreferenceController : BaseController
    {
        readonly INotificationPreferenceBusiness notificationPreferenceBusiness;
        public NotificationPreferenceController(INotificationPreferenceBusiness _notificationPreferenceBusiness)
        {
            notificationPreferenceBusiness = _notificationPreferenceBusiness;
        }

        [HttpPost]
        public ResponseDto<List<NotificationPreferenceListDto>> GetList()
        {
            ResponseBo<List<NotificationPreferenceListBo>> responseBo = notificationPreferenceBusiness.GetList(base.ToBaseBo());

            ResponseDto<List<NotificationPreferenceListDto>> responseDto = new ResponseDto<List<NotificationPreferenceListDto>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = responseBo.Bo.
                    GroupBy(x => x.RelatedPersonId).Select(x => x.First()).
                    Select(
                    x => new NotificationPreferenceListDto()
                    {
                        RelatedPersonId = x.RelatedPersonId,
                        RelatedPersonFullName = x.RelatedPersonFullName,
                        RelatedPersonTypeId = x.RelatedPersonTypeId,
                        RelationTypeId = x.RelationTypeId,
                        TypeList = responseBo.Bo.
                                   Where(y => y.RelatedPersonId == x.RelatedPersonId).
                                   GroupBy(y => y.NotificationPreferenceTypeId).Select(y => y.First())
                                   .Select(
                                            y => new NotificationPreferenceTypeListDto()
                                            {
                                                NotificationPreferenceTypeId = y.NotificationPreferenceTypeId,
                                                SubList = responseBo.Bo.Where(z => z.RelatedPersonId == y.RelatedPersonId
                                                && z.NotificationPreferenceTypeId == y.NotificationPreferenceTypeId).Select(
                                                    f => new NotificationPreferenceListSubDto()
                                                    {
                                                        Id = f.Id,
                                                        NotificationChannelId = f.NotificationChannelId,
                                                        Preference = f.Preference
                                                    }).ToList()
                                            }
                                        ).OrderBy(o => o.NotificationPreferenceTypeId).ToList()
                    }).OrderBy(o => o.RelatedPersonTypeId).ThenBy(o => o.RelatedPersonFullName).ToList();
            }

            responseDto.IsSuccess = true;

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Save(NotificationPreferenceSaveDto saveDto)
        {
            NotificationPreferenceSaveBo seenBo = new NotificationPreferenceSaveBo()
            {
                PreferenceList = saveDto.PreferenceList.
                Select(x => new NotificationPreferenceBo()
                {
                    Id = x.Id,
                    Preference = x.Preference
                }).ToList(),

                Session = Session
            };

            ResponseBo responseBo = notificationPreferenceBusiness.Save(seenBo);

            return responseBo.ToResponseDto();
        }
    }
}