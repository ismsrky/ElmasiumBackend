using Dapper;
using Mh.Business.Bo.Pos;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Pos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Pos
{
    public class PosBusiness : BaseBusiness, IPosBusiness
    {
        public ResponseBo<List<PosProductShortCutListBo>> GetShortCutList(long shopId, long operatorRealId, Enums.Languages languageId)
        {
            ResponseBo<List<PosProductShortCutListBo>> responseBo = new ResponseBo<List<PosProductShortCutListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ShopId", shopId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", operatorRealId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", languageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PosProductShortCutListBo>("spPosProductShortCutList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<List<PosProductShortCutListBo>>();
            }

            return responseBo;
        }

        public ResponseBo SaveShortCut(PosProductShortCutBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    p.Add("@ReturnedId", dbType: DbType.Int64, direction: ParameterDirection.Output);

                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);

                    p.Add("@ShopId", saveBo.ShopId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ProductId", saveBo.ProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@GroupId", saveBo.GroupId, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spPosProductShortCutSave", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo DeleteShortCut(PosProductShortCutBo deleteBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@LanguageId", deleteBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@OperatorRealId", deleteBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);

                    p.Add("@Id", deleteBo.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ShopId", deleteBo.ShopId, DbType.Int64, ParameterDirection.Input);
                    //p.Add("@ProductId", posProductShortCutBo.ProductId, DbType.Int64, ParameterDirection.Input); // we dont need this.
                    p.Add("@GroupId", deleteBo.GroupId, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spPosProductShortCutDelete", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo SaveShortCutGroup(PosProductShortCutGroupBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    p.Add("@ReturnedId", dbType: DbType.Int64, direction: ParameterDirection.Output);

                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);

                    p.Add("@Id", saveBo.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@Name", saveBo.Name, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@ShopId", saveBo.ShopId, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spPosProductShortCutGroupSave", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo DeleteShortCutGroup(PosProductShortCutGroupBo deleteBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@LanguageId", deleteBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@OperatorRealId", deleteBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);

                    p.Add("@Id", deleteBo.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ShopId", deleteBo.ShopId, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spPosProductShortCutGroupDelete", p, commandType: CommandType.StoredProcedure);
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
    }
}