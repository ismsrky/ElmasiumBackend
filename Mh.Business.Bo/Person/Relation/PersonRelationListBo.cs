using Mh.Business.Bo.Person.Balance;
using System.Collections.Generic;

namespace Mh.Business.Bo.Person.Relation
{
    public class PersonRelationListBo
    {
        public int Id { get; set; }

        public int RelatedPersonId { get; set; }
        public Enums.PersonTypes RelatedPersonTypeId { get; set; }
        public string RelatedPersonFullName { get; set; }

        public Enums.Currencies RelatedPersonDefaultCurrencyId { get; set; }
        public string RelatedPersonUrlName { get; set; }

        public bool IsMaster { get; set; }

        public bool IsAlone { get; set; }

        public decimal Balance { get; set; }
        public Enums.BalanceStats BalanceStatId { get; set; }


        public Enums.RelationTypes RelationTypeId { get; set; }

        public long? ApprovalRelationId { get; set; }

        public Enums.ApprovalStats ApprovalStatId { get; set; }
    }
}