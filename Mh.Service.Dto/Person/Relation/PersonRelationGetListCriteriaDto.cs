using System.Collections.Generic;

namespace Mh.Service.Dto.Person.Relation
{
    public class PersonRelationGetListCriteriaDto
    {
        public long PersonId { get; set; }
        public Enums.Currencies CurrencyId { get; set; }

        public List<Enums.RelationTypes> RelationTypeIdList { get; set; }

        public bool IsOppositeOperation { get; set; }

        public bool SearchRelationTypeOpp { get; set; } // make this true only when person search.

        public string Name { get; set; }

        public int PageOffSet { get; set; }

        /// <summary>
        /// If you pass this param, sp will ignore most of params and just will search due given id.
        /// and will return just only one row.
        /// </summary>
        public long? PersonRelationId { get; set; }

        public List<Enums.BalanceStats> BalanceStatIdList { get; set; } // null means all.
    }
}