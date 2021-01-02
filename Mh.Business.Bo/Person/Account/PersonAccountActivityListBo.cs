﻿using System;

namespace Mh.Business.Bo.Person.Account
{
    public class PersonAccountActivityListBo
    {
        public long Id { get; set; }
        public decimal Total { get; set; }

        public long AccountId { get; set; }
        public string AccountName { get; set; }
        public Enums.AccountTypes AccountTypeId { get; set; }
        public bool IsDebt { get; set; } // debt or credit. No other options avabile.

        public long OwnerPersonId { get; set; } // Person whose owner this money account.

        public long FicheId { get; set; } // which fiche does this activity belong?
        public Enums.Currencies FicheCurrencyId { get; set; }
        public Enums.ApprovalStats FicheApprovalStatId { get; set; }
        public DateTime FicheIssueDate { get; set; }
    }
}