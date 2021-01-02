using System;
using System.Collections.Generic;

namespace Mh.Business.Bo.Person.Shop
{
    public class ShopProfileBo
    {
        // These info must be public. Do not include private info here later.
        public long ShopId { get; set; }
        public string UrlName { get; set; }

        public string ShortName { get; set; } //Sign name of the shop

        public string ShopTypeName { get; set; }
        public bool ShopIsAvailable { get; set; }
        public bool ShopIsFoodBeverage { get; set; }

        public long StarCount { get; set; }
        public long StarSum { get; set; }

        public Guid? PortraitImageUniqueId { get; set; }
        public Enums.FileTypes? PortraitImageFileTypeId { get; set; }

        public bool TakesOrder { get; set; }
        public bool TakesOrderOutTime { get; set; }

        public List<Enums.AccountTypes> OrderAccountList { get; set; }
        public string OrderAccountListRawJson { get; set; }

        public List<Enums.Currencies> OrderCurrencyList { get; set; }
        public string OrderCurrencyListRawJson { get; set; }

        public string WorkingHoursTodayStr { get; set; }
        public bool HasWorkingHours { get; set; }

        public bool IsShopOwner { get; set; }

        public decimal? OrderMinPrice { get; set; }
        public Enums.OrderDeliveryTimes? OrderDeliveryTimeId { get; set; }
        public Enums.Currencies? OrderCurrencyId { get; set; }

        public long? AddressId { get; set; }
        public string AddressCountryName { get; set; }
        public string AddressStateName { get; set; }
        public string AddressCityName { get; set; }
        public string AddressDistrictName { get; set; }
        public string AddressLocalityName { get; set; }
        public string AddressNotes { get; set; }
        public string AddressZipCode { get; set; }
        public bool HasAddress { get; set; }

        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
    }
}