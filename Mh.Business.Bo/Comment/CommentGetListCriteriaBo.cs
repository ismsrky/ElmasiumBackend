using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Comment
{
    public class CommentGetListCriteriaBo : BaseBo
    {
        public int CaseId { get; set; }

        public Enums.CommentTypes? CommentTypeId { get; set; }
        public Enums.CommentSortTypes CommentSortTypeId { get; set; }

        public long? ProductId { get; set; }
        public long? PersonId { get; set; }

        public long? CommentId { get; set; }

        public int PageOffSet { get; set; }
    }
}