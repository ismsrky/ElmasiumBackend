namespace Mh.Service.Dto.Fiche
{
    public class FicheApprovalHistoryListDto
    {
        public long Id { get; set; }
        public Enums.ApprovalStats ApprovalStatId { get; set; }
        public long PersonId { get; set; }
        public string PersonFullName { get; set; }
        public double CreateDateNumber { get; set; }
    }
}