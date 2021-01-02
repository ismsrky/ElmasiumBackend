using Dapper;
using Mh.Business.Bo.Dictionary;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Dictionary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Dictionary
{
    public class LanguageBusiness : BaseBusiness, ILanguageBusiness
    {
        public ResponseBo<List<LanguageBo>> GetList()
        {
            ResponseBo<List<LanguageBo>> responseBo = new ResponseBo<List<LanguageBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    responseBo.Bo = conn.Query<LanguageBo>("select Id,[Name],CultureCode from [Language] Where StatId=1").ToList();

                    responseBo.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<List<LanguageBo>>();
            }

            return responseBo;
        }
    }
}