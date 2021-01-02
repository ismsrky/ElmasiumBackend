using Mh.Business.Bo.Comment;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Comment
{
    public interface ICommentBusiness
    {
        ResponseBo Save(CommentBo saveBo);
        ResponseBo<CommentBo> Get(CommentGetCriteriaBo criteriaBo);
        ResponseBo Delete(CommentDeleteBo deleteBo);

        ResponseBo<List<CommentListBo>> GetList(CommentGetListCriteriaBo criteriaBo);
        ResponseBo GetListCount(CommentGetListCriteriaBo criteriaBo);

        ResponseBo SaveLike(CommentLikeBo saveBo);

        ResponseBo<CommentActionsBo> GetActions(CommentGetActionsCriteriaBo criteriaBo);
    }
}