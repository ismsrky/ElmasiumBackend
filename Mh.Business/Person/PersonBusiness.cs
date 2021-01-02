using Dapper;
using Mh.Business.Bo.Person;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Person
{
    public class PersonBusiness : BaseBusiness, IPersonBusiness
    {
        public ResponseBo<List<PersonListBo>> GetList(PersonGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PersonListBo>> responseBo = new ResponseBo<List<PersonListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonTypeIdList", criteriaBo.PersonTypeIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 50);
                    p.Add("@CurrencyId", criteriaBo.CurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonListBo>("spPersonList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    foreach (var item in responseBo.Bo)
                    {
                        item.BalanceStatId = item.Balance == 0 ? Enums.BalanceStats.xZero : item.Balance > 0 ? Enums.BalanceStats.xCreditor : Enums.BalanceStats.xDebtor;
                        item.Balance = Math.Abs(item.Balance);
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PersonListBo>>();
            }

            return responseBo;
        }

        public ResponseBo<PersonNotificationSummaryBo> GetNotificationSummary(BaseBo baseBo)
        {
            ResponseBo<PersonNotificationSummaryBo> responseBo = new ResponseBo<PersonNotificationSummaryBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ApiSessionId", baseBo.Session.ApiSessionId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@MyPersonId", baseBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", baseBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", baseBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonNotificationSummaryBo>("spPersonNotificationSummaryGet2", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, baseBo).ToResponse<PersonNotificationSummaryBo>();
            }

            return responseBo;
        }

        public ResponseBo<List<PersonNavMenuBo>> GetNavMenuList(BaseBo baseBo)
        {
            ResponseBo<List<PersonNavMenuBo>> responseBo = new ResponseBo<List<PersonNavMenuBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@MyPersonId", baseBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", baseBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", baseBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonNavMenuBo>("spPersonNavMenuList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, baseBo).ToResponse<List<PersonNavMenuBo>>();
            }

            return responseBo;
        }
    }
}