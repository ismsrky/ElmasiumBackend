using Mh.Business.Bo.Comment;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Comment;
using Mh.Service.Dto.Comment;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System.Collections.Generic;
using System.Web.Http;

namespace Mh.Service.WebApi.Comment
{
    public class CommentController : BaseController
    {
        readonly ICommentBusiness commentBusiness;
        public CommentController(ICommentBusiness _commentBusiness)
        {
            commentBusiness = _commentBusiness;
        }

        [HttpPost]
        public ResponseDto<CommentDto> Get(CommentGetCriteriaDto criteriaDto)
        {
            CommentGetCriteriaBo criteriaBo = new CommentGetCriteriaBo()
            {
                CommentId = criteriaDto.CommentId,

                Session = Session
            };

            ResponseBo<CommentBo> responseBo = commentBusiness.Get(criteriaBo);

            ResponseDto<CommentDto> responseDto = responseBo.ToResponseDto<CommentDto, CommentBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new CommentDto()
                {
                    Id = responseBo.Bo.Id,
                    OrderId = responseBo.Bo.OrderId,

                    CommentTypeId = responseBo.Bo.CommentTypeId,
                    OrderProductId = responseBo.Bo.OrderProductId,
                    PersonId = responseBo.Bo.PersonId,

                    Comment = responseBo.Bo.Comment,
                    Star = responseBo.Bo.Star,

                    RelatedCommentId = responseBo.Bo.RelatedCommentId
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<CommentListDto>> GetList(CommentGetListCriteriaDto criteriaDto)
        {
            CommentGetListCriteriaBo criteriaBo = new CommentGetListCriteriaBo()
            {
                CaseId = criteriaDto.CaseId,

                CommentTypeId = criteriaDto.CommentTypeId,
                CommentSortTypeId = criteriaDto.CommentSortTypeId,

                ProductId = criteriaDto.ProductId,
                PersonId = criteriaDto.PersonId,
                CommentId = criteriaDto.CommentId,

                PageOffSet = criteriaDto.PageOffSet,

                Session = Session
            };

            ResponseBo<List<CommentListBo>> responseBo = commentBusiness.GetList(criteriaBo);

            ResponseDto<List<CommentListDto>> responseDto = responseBo.ToResponseDto<List<CommentListDto>, List<CommentListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<CommentListDto>();
                foreach (CommentListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new CommentListDto()
                    {
                        Id = itemBo.Id,

                        CommentTypeId = itemBo.CommentTypeId,
                        IsAuthorSeller = itemBo.IsAuthorSeller,
                        AuthorPersonFullName = itemBo.AuthorPersonFullName,

                        LanguageId = itemBo.LanguageId,
                        Comment = itemBo.Comment,
                        Star = itemBo.Star,

                        OrderId = itemBo.OrderId,
                        ProductId = itemBo.ProductId,
                        PersonId = itemBo.PersonId,
                        OrderProductId = itemBo.OrderProductId,

                        PersonFullName = itemBo.PersonFullName,

                        ProductName = itemBo.ProductName,
                        ProductTypeId = itemBo.ProductTypeId,

                        CreateDateNumber = itemBo.CreateDate.ToNumberFromDateTime(),
                        UpdateDateNumber = itemBo.UpdateDate.ToNumberFromDateTimeNull(),

                        LikeCount = itemBo.LikeCount,
                        DislikeCount = itemBo.DislikeCount,

                        MyLike = itemBo.MyLike,

                        RelatedCommentId = itemBo.RelatedCommentId
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto GetListCount(CommentGetListCriteriaDto criteriaDto)
        {
            ResponseDto responseDto = new ResponseDto();

            CommentGetListCriteriaBo criteriaBo = new CommentGetListCriteriaBo()
            {
                CaseId = criteriaDto.CaseId,

                CommentTypeId = criteriaDto.CommentTypeId,

                ProductId = criteriaDto.ProductId,
                PersonId = criteriaDto.PersonId,

                Session = Session
            };

            ResponseBo responseBo = commentBusiness.GetListCount(criteriaBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Delete(CommentDeleteDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            CommentDeleteBo deleteBo = new CommentDeleteBo()
            {
                CommentId = deleteDto.CommentId,

                Session = Session
            };

            ResponseBo responseBo = commentBusiness.Delete(deleteBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Save(CommentDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            CommentBo saveBo = new CommentBo()
            {
                Id = saveDto.Id,
                OrderId = saveDto.OrderId,

                CommentTypeId = saveDto.CommentTypeId,
                OrderProductId = saveDto.OrderProductId,
                PersonId = saveDto.PersonId,

                Comment = saveDto.Comment,
                Star = saveDto.Star,

                RelatedCommentId = saveDto.RelatedCommentId,

                Session = Session
            };

            ResponseBo responseBo = commentBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto SaveLike(CommentLikeDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            CommentLikeBo saveBo = new CommentLikeBo()
            {
                CommentId = saveDto.CommentId,
                IsLike = saveDto.IsLike,

                Session = Session
            };

            ResponseBo responseBo = commentBusiness.SaveLike(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<CommentActionsDto> GetActions(CommentGetActionsCriteriaDto criteriaDto)
        {
            CommentGetActionsCriteriaBo criteriaBo = new CommentGetActionsCriteriaBo()
            {
                CommentId = criteriaDto.CommentId,

                Session = Session
            };

            ResponseBo<CommentActionsBo> responseBo = commentBusiness.GetActions(criteriaBo);

            ResponseDto<CommentActionsDto> responseDto = responseBo.ToResponseDto<CommentActionsDto, CommentActionsBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new CommentActionsDto()
                {
                    Deletable = responseBo.Bo.Deletable,
                    Editable = responseBo.Bo.Editable,
                    Replyable = responseBo.Bo.Replyable
                };
            }

            return responseDto;
        }
    }
}