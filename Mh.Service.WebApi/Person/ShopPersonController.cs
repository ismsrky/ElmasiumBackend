using Mh.Business.Bo.Person.Shop;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Service.Dto.Person.Shop;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mh.Service.WebApi.Person
{
    public class ShopPersonController : BaseController
    {
        readonly IShopPersonBusiness shopPersonBusiness;

        public ShopPersonController(IShopPersonBusiness _shopPersonBusiness)
        {
            shopPersonBusiness = _shopPersonBusiness;
        }

        [HttpPost]
        public ResponseDto Register(RegisterShopDto registerShopDto)
        {
            ResponseDto responseDto = new ResponseDto();

            RegisterShopBo registerShopBo = new RegisterShopBo()
            {
                ShortName = registerShopDto.ShortName,
                ShopTypeId = registerShopDto.ShopTypeId,
                DefaultCurrencyId = registerShopDto.DefaultCurrencyId,

                Session = Session
            };

            responseDto = shopPersonBusiness.Register(registerShopBo).ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto UpdateGeneral(ShopGeneralDto updateDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ShopGeneralBo shopGeneralBo = new ShopGeneralBo()
            {
                ShopId = updateDto.ShopId,
                FullName = updateDto.FullName,
                ShortName = updateDto.ShortName,
                ShopTypeId = updateDto.ShopTypeId,

                IsIncludingVatPurhasePrice = updateDto.IsIncludingVatPurhasePrice,
                IsIncludingVatSalePrice = updateDto.IsIncludingVatSalePrice,

                TaxOffice = updateDto.TaxOffice,
                TaxNumber = updateDto.TaxNumber,

                DefaultCurrencyId = updateDto.DefaultCurrencyId,

                UrlName = updateDto.UrlName,

                Phone = updateDto.Phone,
                Phone2 = updateDto.Phone2,
                Email = updateDto.Email,

                Session = Session
            };

            responseDto = shopPersonBusiness.UpdateGeneral(shopGeneralBo).ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<ShopGeneralDto> GetGeneral(ShopGeneralGetCriteriaDto criteriaDto)
        {
            ShopGeneralGetCriteriaBo criteriaBo = new ShopGeneralGetCriteriaBo()
            {
                PersonId = criteriaDto.PersonId,

                Session = Session
            };

            ResponseBo<ShopGeneralBo> responseBo = shopPersonBusiness.GetGeneral(criteriaBo);

            ResponseDto<ShopGeneralDto> responseDto = responseBo.ToResponseDto<ShopGeneralDto, ShopGeneralBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new ShopGeneralDto()
                {
                    ShopId = criteriaDto.PersonId,

                    FullName = responseBo.Bo.FullName,
                    ShortName = responseBo.Bo.ShortName,
                    ShopTypeId = responseBo.Bo.ShopTypeId,

                    IsIncludingVatPurhasePrice = responseBo.Bo.IsIncludingVatPurhasePrice,
                    IsIncludingVatSalePrice = responseBo.Bo.IsIncludingVatSalePrice,

                    TaxOffice = responseBo.Bo.TaxOffice,
                    TaxNumber = responseBo.Bo.TaxNumber,

                    DefaultCurrencyId = responseBo.Bo.DefaultCurrencyId,

                    UrlName = responseBo.Bo.UrlName,

                    Phone = responseBo.Bo.Phone,
                    Phone2 = responseBo.Bo.Phone2,
                    Email = responseBo.Bo.Email
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto UpdateWorkinghours(ShopWorkingHoursDto updateDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ShopWorkingHoursBo updateBo = new ShopWorkingHoursBo()
            {
                PersonId = updateDto.PersonId,

                MonStartEnd = updateDto.MonStartEnd,
                TuesStartEnd = updateDto.TuesStartEnd,
                WedStartEnd = updateDto.WedStartEnd,
                ThursStartEnd = updateDto.ThursStartEnd,
                FriStartEnd = updateDto.FriStartEnd,
                SatStartEnd = updateDto.SatStartEnd,
                SunStartEnd = updateDto.SunStartEnd,

                TakesOrderOutTime = updateDto.TakesOrderOutTime,

                Session = Session
            };

            responseDto = shopPersonBusiness.UpdateWorkingHours(updateBo).ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<ShopWorkingHoursDto> GetWorkingHours(ShopWorkingHoursGetCriteriaDto criteriaDto)
        {
            ShopWorkingHoursGetCriteriaBo criteriaBo = new ShopWorkingHoursGetCriteriaBo()
            {
                PersonId = criteriaDto.PersonId,

                Session = Session
            };

            ResponseBo<ShopWorkingHoursBo> responseBo = shopPersonBusiness.GetWorkingHours(criteriaBo);

            ResponseDto<ShopWorkingHoursDto> responseDto = responseBo.ToResponseDto<ShopWorkingHoursDto, ShopWorkingHoursBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new ShopWorkingHoursDto()
                {
                    PersonId = responseBo.Bo.PersonId,

                    MonStartEnd = responseBo.Bo.MonStartEnd,
                    TuesStartEnd = responseBo.Bo.TuesStartEnd,
                    WedStartEnd = responseBo.Bo.WedStartEnd,
                    ThursStartEnd = responseBo.Bo.ThursStartEnd,
                    FriStartEnd = responseBo.Bo.FriStartEnd,
                    SatStartEnd = responseBo.Bo.SatStartEnd,
                    SunStartEnd = responseBo.Bo.SunStartEnd,

                    TakesOrderOutTime = responseBo.Bo.TakesOrderOutTime
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto FullNameExists([FromBody]JObject paramss)
        {
            ResponseDto responseDto = new ResponseDto();

            var fullNameParam = paramss.GetValue("fullname");
            string fullName = fullNameParam.Value<string>();

            ResponseBo responseBo = shopPersonBusiness.FullNameExists(fullName, Session.RealPerson.LanguageId);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<ShopProfileDto> GetProfile(ShopProfileGetCriteriaDto criteriaDto)
        {
            ShopProfileGetCriteriaBo criteriaBo = new ShopProfileGetCriteriaBo
            {
                UrlName = criteriaDto.UrlName,

                Session = Session
            };

            ResponseBo<ShopProfileBo> responseBo = shopPersonBusiness.GetProfile(criteriaBo);

            ResponseDto<ShopProfileDto> responseDto = responseBo.ToResponseDto<ShopProfileDto, ShopProfileBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new ShopProfileDto()
                {
                    ShopId = responseBo.Bo.ShopId,
                    UrlName = responseBo.Bo.UrlName,
                    ShortName = responseBo.Bo.ShortName,

                    ShopTypeName = responseBo.Bo.ShopTypeName,
                    ShopIsAvailable = responseBo.Bo.ShopIsAvailable,
                    ShopIsFoodBeverage = responseBo.Bo.ShopIsFoodBeverage,

                    StarCount = responseBo.Bo.StarCount,
                    StarSum = responseBo.Bo.StarSum,

                    TakesOrder = responseBo.Bo.TakesOrder,
                    TakesOrderOutTime = responseBo.Bo.TakesOrderOutTime,

                    OrderAccountList = responseBo.Bo.OrderAccountList,
                    OrderCurrencyList = responseBo.Bo.OrderCurrencyList,

                    WorkingHoursTodayStr = responseBo.Bo.WorkingHoursTodayStr,
                    HasWorkingHours = responseBo.Bo.HasWorkingHours,

                    PortraitImageUniqueIdStr = base.GetImageName(responseBo.Bo.PortraitImageUniqueId, responseBo.Bo.PortraitImageFileTypeId),

                    IsShopOwner = responseBo.Bo.IsShopOwner,

                    OrderMinPrice = responseBo.Bo.OrderMinPrice,
                    OrderDeliveryTimeId = responseBo.Bo.OrderDeliveryTimeId,
                    OrderCurrencyId = responseBo.Bo.OrderCurrencyId,

                    AddressId = responseBo.Bo.AddressId,
                    AddressCountryName = responseBo.Bo.AddressCountryName,
                    AddressStateName = responseBo.Bo.AddressStateName,
                    AddressCityName = responseBo.Bo.AddressCityName,
                    AddressDistrictName = responseBo.Bo.AddressDistrictName,
                    AddressLocalityName = responseBo.Bo.AddressLocalityName,
                    AddressNotes = responseBo.Bo.AddressNotes,
                    AddressZipCode = responseBo.Bo.AddressZipCode,
                    HasAddress = responseBo.Bo.HasAddress,

                    Phone = responseBo.Bo.Phone,
                    Phone2 = responseBo.Bo.Phone2,
                    Email = responseBo.Bo.Email
                };
            }

            return responseDto;
        }


        [HttpPost]
        public ResponseDto UpdateOrderGeneral(ShopOrderGeneralDto updateDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ShopOrderGeneralBo updateBo = new ShopOrderGeneralBo()
            {
                PersonId = updateDto.PersonId,

                TakesOrder = updateDto.TakesOrder,
                OrderAccountList = updateDto.OrderAccountList,
                OrderCurrencyList = updateDto.OrderCurrencyList,

                Session = Session
            };

            responseDto = shopPersonBusiness.UpdateOrderGeneral(updateBo).ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<ShopOrderGeneralDto> GetOrderGeneral(ShopOrderGeneralGetCriteriaDto criteriaDto)
        {
            ShopOrderGeneralGetCriteriaBo criteriaBo = new ShopOrderGeneralGetCriteriaBo
            {
                PersonId = criteriaDto.PersonId,

                Session = Session
            };

            ResponseBo<ShopOrderGeneralBo> responseBo = shopPersonBusiness.GetOrderGeneral(criteriaBo);

            ResponseDto<ShopOrderGeneralDto> responseDto = responseBo.ToResponseDto<ShopOrderGeneralDto, ShopOrderGeneralBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new ShopOrderGeneralDto()
                {
                    PersonId = responseBo.Bo.PersonId,

                    TakesOrder = responseBo.Bo.TakesOrder,
                    OrderAccountList = responseBo.Bo.OrderAccountList,
                    OrderCurrencyList = responseBo.Bo.OrderCurrencyList
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto SaveOrderArea(ShopOrderAreaDto saveDto)
        {
            if (saveDto == null || saveDto.SubList == null || saveDto.SubList.Count == 0)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Message = Business.Stc.GetDicValue("xInvalidData", Session.RealPerson.LanguageId)
                };
            }

            ResponseDto responseDto = new ResponseDto();

            ShopOrderAreaBo saveBo = new ShopOrderAreaBo()
            {
                PersonId = saveDto.PersonId,

                Id = saveDto.Id,

                AddressBoundaryId = saveDto.AddressBoundaryId,
                AddressCountryId = saveDto.AddressCountryId,
                AddressStateId = saveDto.AddressStateId,
                AddressCityId = saveDto.AddressCityId,
                AddressDistrictId = saveDto.AddressDistrictId,

                CurrencyId = saveDto.CurrencyId,
                OrderDeliveryTypeId = saveDto.OrderDeliveryTypeId,

                SubList = (from x in saveDto.SubList
                           select new ShopOrderAreaSubBo
                           {
                               AddressCountryId = x.AddressCountryId,
                               AddressStateId = x.AddressStateId,
                               AddressCityId = x.AddressCityId,
                               AddressDistrictId = x.AddressDistrictId,
                               AddressLocalityId = x.AddressLocalityId,

                               OrderMinPrice = x.OrderMinPrice,
                               OrderDeliveryTimeId = x.OrderDeliveryTimeId
                           }).ToList(),

                Session = Session
            };

            ResponseBo responseBo = shopPersonBusiness.SaveOrderArea(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<ShopOrderAreaListDto>> GetOrderAreaList(ShopOrderAreaGetListCriteriaDto criteriaDto)
        {
            ShopOrderAreaGetListCriteriaBo criteriaBo = new ShopOrderAreaGetListCriteriaBo()
            {
                PersonId = criteriaDto.PersonId,

                Session= Session
            };

            ResponseBo<List<ShopOrderAreaListBo>> responseBo = shopPersonBusiness.GetOrderAreaList(criteriaBo);

            ResponseDto<List<ShopOrderAreaListDto>> responseDto = responseBo.ToResponseDto<List<ShopOrderAreaListDto>, List<ShopOrderAreaListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<ShopOrderAreaListDto>();
                foreach (ShopOrderAreaListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new ShopOrderAreaListDto()
                    {
                        Id = itemBo.Id,

                        AddressBoundaryId = itemBo.AddressBoundaryId,
                        AddressName = itemBo.AddressName,

                        CurrencyId = itemBo.CurrencyId,
                        OrderDeliveryTypeId = itemBo.OrderDeliveryTypeId,

                        SubList = (from x in itemBo.SubList
                                   select new ShopOrderAreaSubListDto
                                   {
                                       Id = x.Id,

                                       AddressName = x.AddressName,

                                       OrderMinPrice = x.OrderMinPrice,
                                       OrderDeliveryTimeId = x.OrderDeliveryTimeId,

                                       HasStates = x.HasStates
                                   }).ToList()
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<ShopOrderAreaDto> GetOrderArea(ShopOrderAreaGetCriteriaDto criteriaDto)
        {
            ShopOrderAreaGetCriteriaBo criteriaBo = new ShopOrderAreaGetCriteriaBo()
            {
                Id = criteriaDto.Id,

                Session = Session
            };

            ResponseBo<ShopOrderAreaBo> responseBo = shopPersonBusiness.GetOrderArea(criteriaBo);

            ResponseDto<ShopOrderAreaDto> responseDto = responseBo.ToResponseDto<ShopOrderAreaDto, ShopOrderAreaBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new ShopOrderAreaDto()
                {
                    PersonId = responseBo.Bo.PersonId,

                    Id = responseBo.Bo.Id,

                    AddressBoundaryId = responseBo.Bo.AddressBoundaryId,
                    AddressCountryId = responseBo.Bo.AddressCountryId,
                    AddressStateId = responseBo.Bo.AddressStateId,
                    AddressCityId = responseBo.Bo.AddressCityId,
                    AddressDistrictId = responseBo.Bo.AddressDistrictId,

                    CurrencyId = responseBo.Bo.CurrencyId,
                    OrderDeliveryTypeId = responseBo.Bo.OrderDeliveryTypeId,

                    SubList = (from x in responseBo.Bo.SubList
                               select new ShopOrderAreaSubDto
                               {
                                   Id = x.Id,

                                   AddressCountryId = x.AddressCountryId,
                                   AddressStateId = x.AddressStateId,
                                   AddressCityId = x.AddressCityId,
                                   AddressDistrictId = x.AddressDistrictId,
                                   AddressLocalityId = x.AddressLocalityId,

                                   OrderMinPrice = x.OrderMinPrice,
                                   OrderDeliveryTimeId = x.OrderDeliveryTimeId
                               }).ToList()
                };
            }

            return responseDto;
        }
        [HttpPost]
        public ResponseDto DeleteOrderArea(ShopOrderAreaDeleteDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ShopOrderAreaDeleteBo deleteBo = new ShopOrderAreaDeleteBo()
            {
                PersonId = deleteDto.PersonId,
                PersonOrderAreaId = deleteDto.PersonOrderAreaId,

                Session = Session
            };

            ResponseBo responseBo = shopPersonBusiness.DeleteOrderArea(deleteBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }
        [HttpPost]
        public ResponseDto DeleteOrderAreaSub(ShopOrderAreaSubDeleteDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ShopOrderAreaSubDeleteBo deleteBo = new ShopOrderAreaSubDeleteBo()
            {
                PersonId = deleteDto.PersonId,
                PersonOrderAreaSubId = deleteDto.PersonOrderAreaSubId,

                Session = Session
            };

            ResponseBo responseBo = shopPersonBusiness.DeleteOrderAreaSub(deleteBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<ShopOrderAccountListDto>> GetOrderAccountList(ShopOrderAccountGetListCriteriaDto criteriaDto)
        {
            ShopOrderAccountGetListCriteriaBo criteriaBo = new ShopOrderAccountGetListCriteriaBo()
            {
                PersonId = criteriaDto.PersonId,

                Session = Session
            };

            ResponseBo<List<ShopOrderAccountListBo>> responseBo = shopPersonBusiness.GetOrderAccountList(criteriaBo);

            ResponseDto<List<ShopOrderAccountListDto>> responseDto = responseBo.ToResponseDto<List<ShopOrderAccountListDto>, List<ShopOrderAccountListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<ShopOrderAccountListDto>();
                foreach (ShopOrderAccountListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new ShopOrderAccountListDto()
                    {
                        Id = itemBo.Id,
                        AccountTypeId = itemBo.AccountTypeId
                    });
                }
            }

            return responseDto;
        }
    }
}