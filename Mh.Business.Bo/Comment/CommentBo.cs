using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Comment
{
    public class CommentBo : BaseBo
    {
        public long? Id { get; set; }
        public long OrderId { get; set; } // can be null in the future.

        public Enums.CommentTypes CommentTypeId { get; set; }
        public long? OrderProductId { get; set; }
        public long? PersonId { get; set; }

        public string Comment { get; set; } // null. Max lenght: 1000
        public byte? Star { get; set; } //not null. Min: 1, Max:5

        public long? RelatedCommentId { get; set; }
    }
}