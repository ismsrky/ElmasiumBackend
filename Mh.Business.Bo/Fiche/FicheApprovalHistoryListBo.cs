using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Bo.Fiche
{
    public class FicheApprovalHistoryListBo
    {
        public long Id { get; set; }
        public Enums.ApprovalStats ApprovalStatId { get; set; }
        public long PersonId { get; set; }
        public string PersonFullName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}