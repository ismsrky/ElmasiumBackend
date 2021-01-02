using Dapper;
using Mh.Business.Bo.Comment;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Comment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Comment
{
    public class CommentBusiness : BaseBusiness, ICommentBusiness
    {
        public ResponseBo Save(CommentBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, saveBo);

                    p.Add("@Id", saveBo.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OrderId", saveBo.OrderId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@CommentTypeId", saveBo.CommentTypeId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OrderProductId", saveBo.OrderProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PersonId", saveBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@Comment", saveBo.Comment, DbType.String, ParameterDirection.Input, 1000);
                    p.Add("@Star", saveBo.Star, DbType.Byte, ParameterDirection.Input);

                    p.Add("@RelatedCommentId", saveBo.RelatedCommentId, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spCommentSave", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                    responseBo.ReturnedId = p.Get<long?>("@ReturnedId");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, saveBo);
            }

            return responseBo;
        }

        public ResponseBo<CommentBo> Get(CommentGetCriteriaBo criteriaBo)
        {
            ResponseBo<CommentBo> responseBo = new ResponseBo<CommentBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@CommentId", criteriaBo.CommentId, DbType.Int64, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<CommentBo>("spCommentGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<CommentBo>();
            }

            return responseBo;
        }

        public ResponseBo Delete(CommentDeleteBo deleteBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, deleteBo);

                    p.Add("@CommentId", deleteBo.CommentId, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spCommentDel", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, deleteBo);
            }

            return responseBo;
        }

        public ResponseBo<List<CommentListBo>> GetList(CommentGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<CommentListBo>> responseBo = new ResponseBo<List<CommentListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@CaseId", criteriaBo.CaseId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@CommentTypeId", criteriaBo.CommentTypeId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@CommentSortTypeId", criteriaBo.CommentSortTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@ProductId", criteriaBo.ProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@CommentId", criteriaBo.CommentId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@PageOffSet", criteriaBo.PageOffSet, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<CommentListBo>("spCommentList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<CommentListBo>>();
            }

            return responseBo;
        }

        public ResponseBo GetListCount(CommentGetListCriteriaBo criteriaBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@CaseId", criteriaBo.CaseId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@CommentTypeId", criteriaBo.CommentTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@ProductId", criteriaBo.ProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spCommentListCount", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                    responseBo.ReturnedId = p.Get<long?>("@ReturnedId");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo);
            }

            return responseBo;
        }

        public ResponseBo SaveLike(CommentLikeBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, saveBo);

                    p.Add("@CommentId", saveBo.CommentId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@IsLike", saveBo.IsLike, DbType.Boolean, ParameterDirection.Input);

                    conn.Execute("spCommentLikeSave", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, saveBo);
            }

            return responseBo;
        }

        public ResponseBo<CommentActionsBo> GetActions(CommentGetActionsCriteriaBo criteriaBo)
        {
            ResponseBo<CommentActionsBo> responseBo = new ResponseBo<CommentActionsBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@CommentId", criteriaBo.CommentId, DbType.Int64, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<CommentActionsBo>("spCommentActionsGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<CommentActionsBo>();
            }

            return responseBo;
        }
    }
}