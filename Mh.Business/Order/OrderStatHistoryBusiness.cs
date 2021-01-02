using Dapper;
using Mh.Business.Bo.Order.StatHistory;
using Mh.Business.Bo.Person;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Order;
using Mh.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Order
{
    public class OrderStatHistoryBusiness : BaseBusiness, IOrderStatHistoryBusiness
    {
        public ResponseBo Save(OrderStatHistoryBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, saveBo);                   
                    p.Add("@NotifyPersonListJson", dbType: DbType.String, direction: ParameterDirection.Output, size: 1000);

                    p.Add("@OrderId", saveBo.OrderId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OrderStatId", saveBo.OrderStatId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@PersonId", saveBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@Notes", saveBo.Notes, DbType.String, ParameterDirection.Input, 255);

                    p.Add("@AccountTypeId", saveBo.AccountTypeId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spOrderStatHistorySave", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                    responseBo.ReturnedId = p.Get<long?>("@ReturnedId");

                    string NotifyPersonListJson = p.Get<string>("@NotifyPersonListJson");
                    if (NotifyPersonListJson.IsNotNull())
                    {
                        responseBo.PersonNotifyList = JsonConvert.DeserializeObject<List<PersonNotifyListBo>>(NotifyPersonListJson);
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, saveBo);
            }

            return responseBo;
        }

        public ResponseBo<OrderStatHistoryBo> Get(OrderStatHistoryGetCriteriaBo criteriaBo)
        {
            ResponseBo<OrderStatHistoryBo> responseBo = new ResponseBo<OrderStatHistoryBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@Id", criteriaBo.Id, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<OrderStatHistoryBo>("spOrderStatHistoryGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<OrderStatHistoryBo>();
            }

            return responseBo;
        }

        public ResponseBo<List<OrderStatHistoryListBo>> GetList(OrderStatHistoryGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<OrderStatHistoryListBo>> responseBo = new ResponseBo<List<OrderStatHistoryListBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@OrderId", criteriaBo.OrderId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<OrderStatHistoryListBo>("spOrderStatHistoryList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<OrderStatHistoryListBo>>();
            }

            return responseBo;
        }
    }
}