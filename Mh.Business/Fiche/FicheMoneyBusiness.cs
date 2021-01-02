using Dapper;
using Mh.Business.Bo.Fiche.Money;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Fiche;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Fiche
{
    public class FicheMoneyBusiness : BaseBusiness, IFicheMoneyBusiness
    {
        public ResponseBo<List<FicheMoneyListBo>> GetList(FicheMoneyGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<FicheMoneyListBo>> responseBo = new ResponseBo<List<FicheMoneyListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@FicheId", criteriaBo.FicheId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<FicheMoneyListBo>("spFicheMoneyList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<FicheMoneyListBo>>();
            }

            return responseBo;
        }
    }
}