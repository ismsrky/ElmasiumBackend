﻿using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.Product
{
    public class PersonProductProfileGetCriteriaBo : BaseBo
    {
        public int CaseId { get; set; } // 0: PersonUrlName and ProductCode mus be given, 1: PersonProductId must be given.

        public string PersonUrlName { get; set; } // not null in case of 0. max length: 50
        public string ProductCode { get; set; } // not null in case of 0. max length: 50

        public long? PersonProductId { get; set; } // not null in case of 1.
    }
}