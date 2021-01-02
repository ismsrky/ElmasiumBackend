namespace Mh.Service.Dto.Comment
{
    public class CommentListDto
    {
        public long Id { get; set; }
        public Enums.CommentTypes CommentTypeId { get; set; }
        public bool IsAuthorSeller { get; set; }
        public string AuthorPersonFullName { get; set; }

        public Enums.Languages LanguageId { get; set; }
        public string Comment { get; set; }
        public int Star { get; set; }

        public long OrderId { get; set; }
        public long? ProductId { get; set; }
        public long? PersonId { get; set; }
        public long? OrderProductId { get; set; }

        public string PersonFullName { get; set; }

        public string ProductName { get; set; }
        public Enums.ProductTypes? ProductTypeId { get; set; }

        public double CreateDateNumber { get; set; }
        public double? UpdateDateNumber { get; set; }

        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

        public bool? MyLike { get; set; }

        public long? RelatedCommentId { get; set; }
    }
}