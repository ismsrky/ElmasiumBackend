using Mh.Service.Dto.Fiche.Money;
using Mh.Service.Dto.Fiche.Product;
using Mh.Service.Dto.Fiche.Relation;
using System.Collections.Generic;

namespace Mh.Service.Dto.Fiche
{
    public class FicheDto
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
        public double IssueDateNumber { get; set; }
        public double? DueDateNumber { get; set; }

        public decimal GrandTotal { get; set; }
        public decimal Total { get; set; }
        public decimal RowDiscountTotal { get; set; }

        public decimal UnderDiscountRate { get; set; }
        public decimal UnderDiscountTotal { get; set; }

        public string Notes { get; set; }

        public long? AcceptorPersonId { get; set; }

        public bool IsUncompleted { get; set; }

        public long? OrderId { get; set; }

        public List<FicheMoneyDto> MoneyList { get; set; }
        public List<FicheProductDto> ProductList { get; set; }
        public List<FicheVatTotalDto> VatTotalList { get; set; }
        public List<FicheRelationSaveDto> RelationList { get; set; }
    }
}