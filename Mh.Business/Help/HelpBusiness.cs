using Dapper;
using Mh.Business.Bo.Help;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Help;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Help
{
    public class HelpBusiness : BaseBusiness, IHelpBusiness
    {
        public ResponseBo<List<HelpListBo>> GetList(HelpGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<HelpListBo>> responseBo = new ResponseBo<List<HelpListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ApplicationTypeId", criteriaBo.ApplicationTypeId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Name", criteriaBo.Name, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<HelpListBo>("spHelpList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<HelpListBo>>();
            }

            return responseBo;
        }
    }
}