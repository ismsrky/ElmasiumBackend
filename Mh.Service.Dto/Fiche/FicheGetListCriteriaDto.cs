using System;
using System.Collections.Generic;

namespace Mh.Service.Dto.Fiche
{
    public class FicheGetListCriteriaDto
    {
        public List<long> OtherPersonsIdList { get; set; } // can be null

        public List<Enums.FicheTypeFakes> FicheTypeFakeIdList { get; set; }
        public List<Enums.ApprovalStats> ApprovalStatIdList { get; set; }
        public List<Enums.FicheContents> FicheContentIdList { get; set; }

        public decimal? GrandTotalMin { get; set; }
        public decimal? GrandTotalMax { get; set; }

        public string PrintedCode { get; set; }

        public double? IssueDateStartNumber { get; set; }
        public double? IssueDateEndNumber { get; set; }

        public Enums.Currencies CurrencyId { get; set; }

        public Enums.PaymentStats? PaymentStatId { get; set; }

        /// <summary>
        /// If you pass this param, sp will ignore other params and just will search due given fiche id.
        /// and will return just only one row.
        /// </summary>
        public long? FicheId { get; set; }

        /// <summary>
        /// If you pass this param, sp will ignore other params and just will search related fiches of given fiche id.
        /// </summary>
        public long? FicheIdRelated { get; set; }

        public long? DebtPersonId { get; set; }
        public long? CreditPersonId { get; set; }

        public List<long> ExcludingFicheIdList { get; set; }

        public int PageOffSet { get; set; }
    }
}