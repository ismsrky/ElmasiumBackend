using Dapper;
using Mh.Business.Bo.Notification.EMail;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Notification;
using Mh.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Notification
{
    public class NotificationEMailBusiness : BaseBusiness, INotificationEMailBusiness
    {
        public ResponseBo SaveSent(NotificationEMailSentSaveBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@NotificationEMailId", saveBo.NotificationEMailId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@SentSuccessfully", saveBo.SentSuccessfully, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@Content", saveBo.Content, DbType.String, ParameterDirection.Input, 100000);

                    conn.Execute("spNotificationEMailSentSave", p, commandType: CommandType.StoredProcedure);
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }

            return responseBo;
        }

        public ResponseBo SaveLog(NotificationEMailLogBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@NotificationEMailId", saveBo.NotificationEMailId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@EmailEventId", saveBo.EmailEventId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@LogExceptionId", saveBo.LogExceptionId, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spNotificationEMailLogSave", p, commandType: CommandType.StoredProcedure);
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }

            return responseBo;
        }

        public ResponseBo PrepareDailySummary()
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    conn.Execute("spNotificationEMailPrepareDailySummary", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }

            return responseBo;
        }

        public ResponseBo<List<NotificationEMailListBo>> GetNotSentList()
        {
            ResponseBo<List<NotificationEMailListBo>> responseBo = new ResponseBo<List<NotificationEMailListBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    responseBo.Bo = conn.Query<NotificationEMailListBo>("spNotificationEMailNotSentList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.IsSuccess && responseBo.Bo != null && responseBo.Bo.Count() > 0)
                    {
                        foreach (NotificationEMailListBo item in responseBo.Bo)
                        {
                            // ReceiverListJson cannot be null.
                            item.ReceiverList = JsonConvert.DeserializeObject<List<NotificationEMailReceiverListBo>>(item.ReceiverListJson);

                            // AttachListJson can be null.
                            if (item.AttachListJson.IsNotNull())
                            {
                                item.AttachList = JsonConvert.DeserializeObject<List<NotificationEMailAttachListBo>>(item.AttachListJson);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<List<NotificationEMailListBo>>();
            }

            return responseBo;
        }
    }
}