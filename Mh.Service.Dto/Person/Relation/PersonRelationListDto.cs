using Mh.Service.Dto.Person.Balance;
using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Relation
{
    public class PersonRelationListDto
    {
        public long Id { get; set; }

        public long RelatedPersonId { get; set; }
        public Enums.PersonTypes RelatedPersonTypeId { get; set; }
        public string RelatedPersonFullName { get; set; }

        public Enums.Currencies RelatedPersonDefaultCurrencyId { get; set; }

        public string RelatedPersonUrlName { get; set; }

        public bool IsMaster { get; set; }

        public bool IsAlone { get; set; }

        public decimal Balance { get; set; }
        public Enums.BalanceStats BalanceStatId { get; set; }

        public List<PersonRelationSubListDto> RelationSubList { get; set; }

        //public List<Enums.RelationTypes> RelationTypeIdList { get; set; }

        //public Enums.ApprovalStats ApprovalStatId { get; set; }
    }
}