using Mh.Business.Contract.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mh.Business.Bo.Person.Account;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Service.Dto.Person.Account;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using Mh.Service.Dto.Help;
using Mh.Business.Bo.Help;

namespace Mh.Service.WebApi.Help
{
    public class HelpController : BaseController
    {
        readonly IHelpBusiness helpBusiness;
        public HelpController(IHelpBusiness _helpBusiness)
        {
            helpBusiness = _helpBusiness;
        }

        [HttpPost]
        [AllowAnonymous]
        public ResponseDto<List<HelpListDto>> GetList(HelpGetListCriteriaDto criteriaDto)
        {
            HelpGetListCriteriaBo criteriaBo = new HelpGetListCriteriaBo()
            {
                ApplicationTypeId = criteriaDto.ApplicationTypeId,
                Name = criteriaDto.Name,

                Session = Session
            };

            ResponseBo <List<HelpListBo>> responseBo = helpBusiness.GetList(criteriaBo);

            ResponseDto<List<HelpListDto>> responseDto = responseBo.ToResponseDto<List<HelpListDto>, List<HelpListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<HelpListDto>();
                foreach (HelpListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new HelpListDto()
                    {
                        CreateDateNumber = itemBo.CreateDate.ToNumberFromDateTime(),
                        UpdateDateNumber = itemBo.UpdateDate.ToNumberFromDateTimeNull(),

                        VideoUrl = itemBo.VideoUrl,
                        ImageUrl = itemBo.ImageUrl,
                        IsTextHtml = itemBo.IsTextHtml,
                        Caption = itemBo.Caption,
                        Text = itemBo.Text,

                        Order = itemBo.Order
                    });
                }
            }

            responseDto.Dto.Add(new HelpListDto()
            {

                IsTextHtml = true,
                Caption = "İletişim",
                Text = " Cep / Whatsapp: +90 506 966 33 11"
                + "<br>Mail: <a href=\"mailto:ismail.sarikaya@elmasium.com\" target=\"_top\"> ismail.sarikaya@elmasium.com</a>"
                + " <br> Adres: Ankara / Mamak"
                + "<br> İstediğiniz zaman arayın,"
                + "mail atın veya buluşalım.",

                Order = 500
            });

            return responseDto;
        }
    }
}