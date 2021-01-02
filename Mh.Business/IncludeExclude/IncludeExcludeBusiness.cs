using Dapper;
using Mh.Business.Bo.IncludeExclude;
using Mh.Business.Bo.Person.Product;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.IncludeExclude;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.IncludeExclude
{
    public class IncludeExcludeBusiness : BaseBusiness, IIncludeExcludeBusiness
    {
        public ResponseBo Save(IncludeExcludeSaveBo saveBo)
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

                    p.Add("@ProductCategoryId", saveBo.ProductCategoryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@IsInclude", saveBo.IsInclude, DbType.Boolean, ParameterDirection.Input);

                    p.Add("@IncludeExcludeNameListStr", saveBo.IncludeExcludeNameListStr, DbType.String, ParameterDirection.Input, int.MaxValue);
                    p.Add("@IncludeExcludeName", saveBo.IncludeExcludeName, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@MyPersonId", saveBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spIncludeExcludeSave", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo SavePersonProduct(PersonProductIncludeExcludeBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                string includeExcludeListJson = null;
                if (saveBo.IncludeExcludeList != null && saveBo.IncludeExcludeList.Count() > 0)
                {
                    includeExcludeListJson = JsonConvert.SerializeObject(saveBo.IncludeExcludeList);
                }

                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonProductId", saveBo.PersonProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@IsInclude", saveBo.IsInclude, DbType.Boolean, ParameterDirection.Input);

                    p.Add("@IncludeExcludeListJson", includeExcludeListJson, DbType.String, ParameterDirection.Input, int.MaxValue);

                    p.Add("@MyPersonId", saveBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spPersonProductIncludeExcludeSave", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo<List<IncludeExcludeBo>> GetList(IncludeExcludeGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<IncludeExcludeBo>> responseBo = new ResponseBo<List<IncludeExcludeBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@CaseId", criteriaBo.CaseId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@IsInclude", criteriaBo.IsInclude, DbType.Boolean, ParameterDirection.Input);

                    p.Add("@ProductCategoryId", criteriaBo.ProductCategoryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Name", criteriaBo.Name, DbType.String, ParameterDirection.Input,50);
                    p.Add("@PageOffSet", criteriaBo.PageOffSet, DbType.Int32, ParameterDirection.Input);

                    p.Add("@PersonProductId", criteriaBo.PersonProductId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<IncludeExcludeBo>("spIncludeExcludeList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<IncludeExcludeBo>>();
            }

            return responseBo;
        }
    }
}