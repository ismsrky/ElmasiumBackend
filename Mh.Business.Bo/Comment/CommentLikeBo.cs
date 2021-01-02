using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Comment
{
    public class CommentLikeBo : BaseBo
    {
        public long CommentId { get; set; }
        public bool IsLike { get; set; }
    }
}