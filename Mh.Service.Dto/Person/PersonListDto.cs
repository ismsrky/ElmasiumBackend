﻿namespace Mh.Service.Dto.Person
{
    public class PersonListDto
    {
        public long Id { get; set; }

        public string FullName { get; set; }
        public Enums.EnumStats StatId { get; set; }
        public Enums.PersonTypes PersonTypeId { get; set; }

        public Enums.Currencies DefaultCurrencyId { get; set; }
        public Enums.ShopTypes? ShopTypeId { get; set; }
        public Enums.RelationTypes MasterRelationTypeId { get; set; }

        public Address.PersonAddressListDto Address { get; set; }

        public decimal Balance { get; set; }
        public Enums.BalanceStats BalanceStatId { get; set; }

        public string UrlName { get; set; }
    }
}