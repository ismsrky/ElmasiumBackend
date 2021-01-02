using Mh.Business.Bo.Person;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Service.Dto.Person;
using Mh.Service.Dto.Person.Address;
using Mh.Service.Dto.Sys;
using Mh.Sessions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mh.Service.WebApi.Person
{
    public class PersonController : BaseController
    {
        readonly IPersonBusiness personBusiness;

        public PersonController(IPersonBusiness _personBusiness)
        {
            personBusiness = _personBusiness;
        }

        [HttpPost]
        public ResponseDto<List<PersonListDto>> GetList(PersonGetListCriteriaDto criteriaDto)
        {

            PersonGetListCriteriaBo criteriaBo = new PersonGetListCriteriaBo()
            {
                PersonTypeIdList = criteriaDto.PersonTypeIdList,
                CurrencyId = criteriaDto.CurrencyId,

                Session = Session
            };

            ResponseBo<List<PersonListBo>> responseBo = personBusiness.GetList(criteriaBo);

            ResponseDto<List<PersonListDto>> responseDto = responseBo.ToResponseDto<List<PersonListDto>, List<PersonListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PersonListDto>();
                foreach (PersonListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new PersonListDto()
                    {
                        Id = itemBo.Id,

                        FullName = itemBo.FullName,
                        StatId = itemBo.StatId,
                        PersonTypeId = itemBo.PersonTypeId,

                        Balance = itemBo.Balance,
                        BalanceStatId = itemBo.BalanceStatId,

                        DefaultCurrencyId = itemBo.DefaultCurrencyId,
                        ShopTypeId = itemBo.ShopTypeId,
                        MasterRelationTypeId = itemBo.MasterRelationTypeId,

                        UrlName = itemBo.UrlName,

                        Address = itemBo.Address == null ? null :
                        new PersonAddressListDto()
                        {
                            Id = itemBo.Address.Id,
                            AddressTypeId = itemBo.Address.AddressTypeId,
                            StatId = itemBo.Address.StatId,
                            Name = itemBo.Address.Name,
                            InvolvedPersonName = itemBo.Address.InvolvedPersonName,

                            CountryName = itemBo.Address.CountryName,
                            StateName = itemBo.Address.StateName,
                            CityName = itemBo.Address.CityName,
                            DistrictName = itemBo.Address.DistrictName,
                            LocalityName = itemBo.Address.LocalityName,

                            Notes = itemBo.Address.Notes,
                            Phone = itemBo.Address.Phone
                        }
                    });
                }
            }

            return responseDto;
        }


        [HttpPost]
        public ResponseDto ChangeSelectedCurrency(PersonChangeSelectedCurrencyDto currencyDto)
        {
            ResponseDto responseDto = new ResponseDto();
            responseDto.IsSuccess = true;

            Session.MyPerson.SelectedCurrencyId = currencyDto.CurrencyId;
            Session.RealPerson.SelectedCurrencyId = currencyDto.CurrencyId;

            //foreach (Session session in SessionManager.SessionList)
            //{
            //    if (session.MyPerson.Id == currencyDto.PersonId)
            //    {
            //        session.MyPerson.DefaultCurrencyId = currencyDto.CurrencyId;
            //    }

            //    if (session.RealPerson.Id == currencyDto.PersonId)
            //    {
            //        session.RealPerson.DefaultCurrencyId = currencyDto.CurrencyId;
            //    }
            //}

            return responseDto;
        }


        [HttpPost]
        public ResponseDto ChangeMyPerson(PersonChangeMyPersonDto myPersonDto)
        {
            ResponseDto responseDto = new ResponseDto();

            Session.MyPerson = PersonRelationController.GetMyPerson(myPersonDto, Session);

            responseDto.IsSuccess = true;

            return responseDto;
        }


        [HttpPost]
        public ResponseDto<PersonNotificationSummaryDto> GetNotificationSummary()
        {
            ResponseBo<PersonNotificationSummaryBo> responseBo = personBusiness.GetNotificationSummary(ToBaseBo());

            ResponseDto<PersonNotificationSummaryDto> responseDto = responseBo.ToResponseDto<PersonNotificationSummaryDto, PersonNotificationSummaryBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new PersonNotificationSummaryDto()
                {
                    PersonId = responseBo.Bo.PersonId,

                    xFicheIncomings = responseBo.Bo.xFicheIncomings,
                    xFicheOutgoings = responseBo.Bo.xFicheOutgoings,

                    xRelationIncomings = responseBo.Bo.xRelationIncomings,
                    xRelationOutgoings = responseBo.Bo.xRelationOutgoings,

                    xNotifications = responseBo.Bo.xNotifications,

                    xIncomingOrders = responseBo.Bo.xIncomingOrders,
                    xOutgoingOrders = responseBo.Bo.xOutgoingOrders,

                    xIncomingOrderReturns = responseBo.Bo.xIncomingOrderReturns,
                    xOutgoingOrderReturns = responseBo.Bo.xOutgoingOrderReturns,

                    xBasket = responseBo.Bo.xBasket
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<PersonNavMenuDto>> GetNavMenuList()
        {
            ResponseBo<List<PersonNavMenuBo>> responseBo = personBusiness.GetNavMenuList(base.ToBaseBo());

            ResponseDto<List<PersonNavMenuDto>> responseDto = responseBo.ToResponseDto<List<PersonNavMenuDto>, List<PersonNavMenuBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = toNavMenuDto(responseBo.Bo, 0);
            }

            return responseDto;
        }

        List<PersonNavMenuDto> toNavMenuDto(List<PersonNavMenuBo> menuListBo, int parentId)
        {
            if (menuListBo == null) return null;
            List<PersonNavMenuDto> menuDto = (from m in menuListBo
                                              where m.ParentId == parentId
                                              orderby m.RangeOrder
                                              select new PersonNavMenuDto()
                                              {
                                                  Id = m.Id,
                                                  Name = m.Name,
                                                  Url = m.Url,
                                                  IconClass = m.IconClass,
                                                  IconColor = m.IconColor,
                                                  SubMenuList = toNavMenuDto(menuListBo, m.Id),
                                                  Range = m.Range,

                                                  Position = m.Position
                                              }).ToList();

            return menuDto;
        }
    }
}