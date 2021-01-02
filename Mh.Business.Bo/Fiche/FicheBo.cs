using Mh.Business.Bo.Fiche.Money;
using Mh.Business.Bo.Fiche.Product;
using Mh.Business.Bo.Fiche.Relation;
using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;

namespace Mh.Business.Bo.Fiche
{
    public class FicheBo : BaseBo
    {
        public long Id { get; set; }
        public long DebtPersonId { get; set; }
        public long CreditPersonId { get; set; }

        public Enums.FicheTypes FicheTypeId { get; set; }
        public Enums.Currencies CurrencyId { get; set; }
        public Enums.ApprovalStats ApprovalStatId { get; set; }
        public bool IncludingVat { get; set; }

        public Enums.FicheContents FicheContentId { get; set; }
        public Enums.FicheContentGroups FicheContentGroupId { get; set; }

        public string PrintedCode { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? DueDate { get; set; }

        public decimal GrandTotal { get; set; }
        public decimal Total { get; set; }
        public decimal RowDiscountTotal { get; set; }

        public decimal UnderDiscountRate { get; set; }
        public decimal UnderDiscountTotal { get; set; }

        public string Notes { get; set; }

        public long? AcceptorPersonId { get; set; }

        public bool IsUncompleted { get; set; }

        public long? OrderId { get; set; }

        public List<FicheMoneyBo> MoneyList { get; set; }
        public List<FicheProductBo> ProductList { get; set; }
        public List<FicheVatTotalBo> VatTotalList { get; set; }
        public List<FicheRelationSaveBo> RelationList { get; set; }
    }
}