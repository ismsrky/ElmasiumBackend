using Mh.Business.Bo.Dashboard.Slider;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Dashboard;
using Mh.Service.Dto.Dashboard.Slider;
using Mh.Service.Dto.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mh.Service.WebApi.Dashboard
{
    public class DashboardSliderController : BaseController
    {
        readonly IDashboardSliderBusiness dashboardSliderBusiness;
        public DashboardSliderController(IDashboardSliderBusiness _dashboardSliderBusiness)
        {
            dashboardSliderBusiness = _dashboardSliderBusiness;
        }

        [HttpPost]
        public ResponseDto<List<DashboardSliderGroupListDto>> GetGroupList()
        {
            ResponseBo<List<DashboardSliderGroupListBo>> responseBo = dashboardSliderBusiness.GetGroupList(base.ToBaseBo());

            ResponseDto<List<DashboardSliderGroupListDto>> responseDto = responseBo.ToResponseDto<List<DashboardSliderGroupListDto>, List<DashboardSliderGroupListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<DashboardSliderGroupListDto>();
                foreach (DashboardSliderGroupListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new DashboardSliderGroupListDto()
                    {
                        Id = itemBo.Id,
                        Name = itemBo.Name,
                        Order = itemBo.Order
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<DashboardSliderListDto>> GetList(DashboardSliderGetListCriteriaDto criteriaDto)
        {
            DashboardSliderGetListCriteriaBo criteriaBo = new DashboardSliderGetListCriteriaBo()
            {
                GroupId = criteriaDto.GroupId,

                Session = Session
            };

            ResponseBo<List<DashboardSliderListBo>> responseBo = dashboardSliderBusiness.GetList(criteriaBo);

            ResponseDto<List<DashboardSliderListDto>> responseDto = responseBo.ToResponseDto<List<DashboardSliderListDto>, List<DashboardSliderListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<DashboardSliderListDto>();
                foreach (DashboardSliderListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new DashboardSliderListDto()
                    {
                        Id = itemBo.Id,
                        Order = itemBo.Order,

                        xText = itemBo.xText,
                        xTextBold = itemBo.xTextBold,
                        xButtonText = itemBo.xButtonText,

                        ColorClass = itemBo.ColorClass,

                        PortraitImageUniqueIdStr = base.GetImageName(itemBo.PortraitImageUniqueId, itemBo.PortraitImageFileTypeId),

                        GroupId = itemBo.GroupId
                    });
                }
            }

            return responseDto;
        }
    }
}