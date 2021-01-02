using Dapper;
using Mh.Business.Bo.Sys;
using Mh.Business.Bo.Warning;
using Mh.Business.Contract.Warning;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Mh.Business.Warning
{
    public class WarningBusiness : BaseBusiness, IWarningBusiness
    {
        public ResponseBo Save(WarningBo saveBo)
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

                    p.Add("@WarningModuleTypeId", saveBo.WarningModuleTypeId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@PersonProductId", saveBo.PersonProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@CommentId", saveBo.CommentId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PersonId", saveBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@WarningTypeId", saveBo.WarningTypeId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Notes", saveBo.Notes, DbType.String, ParameterDirection.Input, 1000);

                    p.Add("@MyPersonId", saveBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spWarningSave", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo SaveCheck(WarningCheckBo saveCheckBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@WarningModuleTypeId", saveCheckBo.WarningModuleTypeId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@PersonProductId", saveCheckBo.PersonProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@CommentId", saveCheckBo.CommentId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PersonId", saveCheckBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", saveCheckBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveCheckBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", saveCheckBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spWarningSaveCheck", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, saveCheckBo);
            }

            return responseBo;
        }
    }
}