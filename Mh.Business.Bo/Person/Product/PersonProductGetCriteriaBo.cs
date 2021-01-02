﻿using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Product
{
    public class PersonProductGetCriteriaBo : BaseBo
    {
        public long? PersonProductId { get; set; } // null. 'ProductId' and 'ProductCode' are ignored if any value represented.

        // One of followings must be filled if 'PersonProductId' is null.
        public long? ProductId { get; set; }   // null
        public string ProductCode { get; set; } // null

        public long PersonId { get; set; } // can be real or shop.

        public Enums.Currencies CurrencyId { get; set; }
    }
}