using Dapper;
using Mh.Business.Bo.Notification.Sms;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Notification;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Notification
{
    public class NotificationSmsBusiness : BaseBusiness, INotificationSmsBusiness
    {
        public ResponseBo SaveSent(NotificationSmsSentSaveBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@NotificationSmsId", saveBo.NotificationSmsId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@SentSuccessfully", saveBo.SentSuccessfully, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@TextMessage", saveBo.TextMessage, DbType.String, ParameterDirection.Input, 1000);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Input);

                    conn.Execute("spNotificationSmsSentSave", p, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }

            return responseBo;
        }

        public ResponseBo<List<NotificationSmsListBo>> GetNotSentList()
        {
            ResponseBo<List<NotificationSmsListBo>> responseBo = new ResponseBo<List<NotificationSmsListBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    responseBo.Bo = conn.Query<NotificationSmsListBo>("spNotificationSmsNotSentList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.IsSuccess && responseBo.Bo != null && responseBo.Bo.Count() > 0)
                    {
                        foreach (NotificationSmsListBo item in responseBo.Bo)
                        {
                            // ReceiverListJson cannot be null.
                            item.ReceiverList = JsonConvert.DeserializeObject<List<NotificationSmsReceiverListBo>>(item.ReceiverListJson);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<List<NotificationSmsListBo>>();
            }

            return responseBo;
        }

        public ResponseBo SaveLog(NotificationSmsLogBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@NotificationSmsId", saveBo.NotificationSmsId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@SmsEventId", saveBo.SmsEventId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@LogExceptionId", saveBo.LogExceptionId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ReturnValue", saveBo.ReturnValue, DbType.String, ParameterDirection.Input, 4000);
                    p.Add("@IsSuccess", saveBo.IsSuccess, DbType.Boolean, ParameterDirection.Input);

                    conn.Execute("spNotificationSmsLogSave", p, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }

            return responseBo;
        }
    }
}